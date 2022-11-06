using MoBro.Plugin.SDK.Models;
using MoBro.Plugin.SDK.Models.Metrics;
using MoBro.Plugin.SDK.Models.Resources;

namespace MoBro.Plugin.SDK.Builders;

/// <summary>
/// Builder to create various <see cref="IMoBroItem"/> that can be registered to the service.
/// </summary>
public static class MoBroItem
{
  /// <summary>
  /// Builds a new <see cref="ICategory"/>
  /// </summary>
  /// <returns>An instance of <see cref="CategoryBuilder"/></returns>
  public static CategoryBuilder.IIdStage CreateCategory() => CategoryBuilder.CreateCategory();

  /// <summary>
  /// Builds a new <see cref="IGroup"/>
  /// </summary>
  /// <returns>An instance of <see cref="GroupBuilder"/></returns>
  public static GroupBuilder.IIdStage CreateGroup() => GroupBuilder.CreateGroup();

  /// <summary>
  /// Builds a new <see cref="IResource"/> (icons, images,...)
  /// </summary>
  /// <returns>An instance of <see cref="ResourceBuilder"/></returns>
  public static ResourceBuilder.IIdStage CreateResource() => ResourceBuilder.CreateResource();

  /// <summary>
  /// Builds a new <see cref="IMetric"/>
  /// </summary>
  /// <returns>An instance of <see cref="MetricBuilder"/></returns>
  public static MetricBuilder.IIdStage CreateMetric() => MetricBuilder.CreateMetric();

  /// <summary>
  /// Builds a new <see cref="IMetricType"/>
  /// </summary>
  /// <returns>An instance of <see cref="TypeBuilder"/></returns>
  public static TypeBuilder.IIdStage CreateMetricType() => TypeBuilder.CreateMetricType();
}