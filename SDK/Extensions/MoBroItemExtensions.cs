using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MoBro.Plugin.SDK.Enums;
using MoBro.Plugin.SDK.Exceptions;
using MoBro.Plugin.SDK.Models;
using MoBro.Plugin.SDK.Models.Categories;
using MoBro.Plugin.SDK.Models.Metrics;
using MoBro.Plugin.SDK.Models.Resources;
using MoBro.Plugin.SDK.Services;
using Action = MoBro.Plugin.SDK.Models.Actions.Action;
using Group = MoBro.Plugin.SDK.Models.Categories.Group;

namespace MoBro.Plugin.SDK.Extensions;

/// <summary>
/// Extension functions for <see cref="IMoBroItem"/>
/// </summary>
internal static class MoBroItemExtensions
{
  private static readonly Regex IdValidationRegex = new(@"^[\w\.\-]+$", RegexOptions.Compiled);

  /// <summary>
  /// Validates an <see cref="IMoBroItem"/>
  /// </summary>
  /// <param name="item">The <see cref="IMoBroItem"/></param>
  /// <param name="mobroService">The <see cref="IMoBroService"/> instance</param>
  /// <returns>The <see cref="IMoBroItem"/></returns>
  /// <exception cref="PluginException">In case the item is invalid</exception>
  internal static IMoBroItem Validate(this IMoBroItem item, IMoBroService mobroService)
  {
    if (!IdValidationRegex.IsMatch(item.Id))
    {
      throw new PluginException($"Invalid id '{item.Id}' for MoBroItem");
    }

    switch (item)
    {
      case Category category:
        ValidateCategory(category, mobroService);
        break;
      case Group group:
        ValidateGroup(group, mobroService);
        break;
      case IResource resource:
        ValidateResource(resource);
        break;
      case MetricType type:
        ValidateMetricType(type, mobroService);
        break;
      case Metric metric:
        ValidateMetric(metric, mobroService);
        break;
      case Action action:
        ValidateAction(action, mobroService);
        break;
      default: throw new PluginException("Unknown item type");
    }

    return item;
  }

  private static void ValidateCategory(Category category, IMoBroService itemRegister)
  {
    if (string.IsNullOrEmpty(category.Label))
    {
      throw new PluginException($"Empty label for category '{category.Id}'");
    }

    if (!IdValidationRegex.IsMatch(category.Id))
    {
      throw new PluginException($"Invalid Id '{category.Id}' for category");
    }

    if (Enum.TryParse<CoreCategory>(category.Id, true, out _))
    {
      throw new PluginException($"Cannot overwrite core category '{category.Id}'");
    }

    if (!string.IsNullOrEmpty(category.Icon) && !itemRegister.TryGet<Icon>(category.Icon, out _))
    {
      throw new PluginException($"Category '{category.Id}' references not registered Icon '{category.Icon}'");
    }
  }

  private static void ValidateGroup(Group group, IMoBroService itemRegister)
  {
    if (string.IsNullOrEmpty(group.Label))
    {
      throw new PluginException($"Empty label for group '{group.Id}'");
    }

    if (!string.IsNullOrEmpty(group.Icon) && !itemRegister.TryGet<Icon>(group.Icon, out _))
    {
      throw new PluginException($"Group '{group.Id}' references not registered Icon '{group.Icon}'");
    }
  }

  private static void ValidateResource(IResource resource)
  {
    switch (resource)
    {
      case Image image:
        if (string.IsNullOrEmpty(image.RelativeFilePath))
        {
          throw new PluginException($"Empty path for Image Resource '{resource.Id}'");
        }

        if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), image.RelativeFilePath)))
        {
          throw new PluginException($"File path does not existfor Image Resource '{resource.Id}'");
        }

        break;

      case Icon icon:

        if (icon.RelativeFilePaths == null || !icon.RelativeFilePaths.Any())
        {
          throw new PluginException($"No icon paths set for Icon Resource '{resource.Id}'");
        }

        if (icon.RelativeFilePaths.Any(p => string.IsNullOrWhiteSpace(p.Value)))
        {
          throw new PluginException($"One or more empty file paths for Icon Resource '{resource.Id}'");
        }

        if (icon.RelativeFilePaths.Any(p => !File.Exists(Path.Combine(Directory.GetCurrentDirectory(), p.Value))))
        {
          throw new PluginException($"One or more file paths do not exist for Icon Resource '{resource.Id}'");
        }

        break;

      default:
        throw new PluginException($"Unknown resource type '{resource.GetType()}' for Resource '{resource.Id}'");
    }
  }

  private static void ValidateMetricType(MetricType type, IMoBroService itemRegister)
  {
    if (string.IsNullOrEmpty(type.Label))
    {
      throw new PluginException($"Empty label for MetricType '{type.Id}'");
    }

    if (Enum.TryParse<CoreMetricType>(type.Id, true, out _))
    {
      throw new PluginException($"Cannot overwrite core MetricType '{type.Id}'");
    }

    if (type.Id.Length == 3 && Enum.TryParse<CoreMetricTypeCurrency>(type.Id, true, out _))
    {
      throw new PluginException($"Cannot overwrite core MetricType currency '{type.Id}'");
    }

    if (!string.IsNullOrEmpty(type.Icon) && !itemRegister.TryGet<Icon>(type.Icon, out _))
    {
      throw new PluginException($"MetricType '{type.Id}' references not registered Icon '{type.Icon}'");
    }

    if (type.BaseUnit is not null)
    {
      ValidateUnit(type.BaseUnit, type.Id);
    }

    if (type.Units is not null)
    {
      foreach (var unit in type.Units)
      {
        ValidateUnit(unit, type.Id);
      }
    }
  }

  private static void ValidateUnit(Unit unit, string typeId)
  {
    if (string.IsNullOrWhiteSpace(unit.Label))
    {
      throw new PluginException($"Empty label for Unit of metric type {typeId}");
    }

    if (string.IsNullOrWhiteSpace(unit.Abbreviation))
    {
      throw new PluginException($"Empty abbreviation for Unit of metric type {typeId}");
    }

    if (string.IsNullOrWhiteSpace(unit.FromBaseFormula) || !unit.FromBaseFormula.Contains('x'))
    {
      throw new PluginException($"Invalid 'FromBase' formula for Unit of metric type {typeId}");
    }

    if (string.IsNullOrWhiteSpace(unit.ToBaseFormula) || !unit.FromBaseFormula.Contains('x'))
    {
      throw new PluginException($"Invalid 'ToBase' formula for Unit of metric type {typeId}");
    }
  }

  private static void ValidateMetric(Metric metric, IMoBroService itemRegister)
  {
    if (string.IsNullOrEmpty(metric.Label))
    {
      throw new PluginException($"Empty label for Metric '{metric.Id}'");
    }

    if (!itemRegister.TryGet<MetricType>(metric.TypeId, out _) && !IsCoreType(metric.TypeId))
    {
      throw new PluginException($"Metric '{metric.Id}' references not registered type '{metric.TypeId}'");
    }

    if (string.IsNullOrWhiteSpace(metric.CategoryId) || !IdValidationRegex.IsMatch(metric.CategoryId))
    {
      throw new PluginException($"Metric '{metric.Id}' references not invalid category id '{metric.CategoryId}'");
    }

    if (!itemRegister.TryGet<Category>(metric.CategoryId, out _) &&
        !Enum.TryParse<CoreCategory>(metric.CategoryId, true, out _))
    {
      throw new PluginException($"Metric '{metric.Id}' references not registered category '{metric.CategoryId}'");
    }

    if (metric.GroupId != null)
    {
      if (!IdValidationRegex.IsMatch(metric.GroupId))
      {
        throw new PluginException($"Metric '{metric.Id}' references invalid group id '{metric.GroupId}'");
      }

      if (!itemRegister.TryGet<Group>(metric.GroupId, out _))
      {
        throw new PluginException($"Metric '{metric.Id}' references not registered group '{metric.GroupId}'");
      }
    }
  }

  private static void ValidateAction(Action action, IMoBroService itemRegister)
  {
    if (string.IsNullOrEmpty(action.Label))
    {
      throw new PluginException($"Empty label for action '{action.Id}'");
    }

    if (string.IsNullOrWhiteSpace(action.CategoryId) || !IdValidationRegex.IsMatch(action.CategoryId))
    {
      throw new PluginException($"Action '{action.Id}' references not invalid category id '{action.CategoryId}'");
    }

    if (!itemRegister.TryGet<Category>(action.CategoryId, out _) &&
        !Enum.TryParse<CoreCategory>(action.CategoryId, true, out _))
    {
      throw new PluginException($"Action '{action.Id}' references not registered category '{action.CategoryId}'");
    }

    if (action.GroupId != null)
    {
      if (!IdValidationRegex.IsMatch(action.GroupId))
      {
        throw new PluginException($"Action '{action.Id}' references invalid group id '{action.GroupId}'");
      }

      if (!itemRegister.TryGet<Group>(action.GroupId, out _))
      {
        throw new PluginException($"Action '{action.Id}' references not registered group '{action.GroupId}'");
      }
    }

    if (action.Handler is null)
    {
      throw new PluginException($"Action '{action.Id}' has no registered handler");
    }
  }

  private static bool IsCoreType(string id)
  {
    return Enum.TryParse<CoreMetricType>(id, true, out _) ||
           (id.Length == 3 && Enum.TryParse<CoreMetricTypeCurrency>(id, true, out _));
  }
}