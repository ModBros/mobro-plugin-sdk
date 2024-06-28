using System;
using System.Globalization;
using System.Text.RegularExpressions;
using MoBro.Plugin.SDK.Enums;
using MoBro.Plugin.SDK.Exceptions;
using MoBro.Plugin.SDK.Models.Metrics;
using MoBro.Plugin.SDK.Models.Resources;
using MoBro.Plugin.SDK.Services;

namespace MoBro.Plugin.SDK.Extensions;

/// <summary>
/// Extension functions for <see cref="MetricValue"/>
/// </summary>
internal static class MetricValueExtensions
{
  private static readonly Regex IdValidationRegex = new(@"^[\w\.\-]+$", RegexOptions.Compiled);

  private static readonly Regex Iso8601DurationRegex = new(
    @"^(-?)P(?=\d|T\d)(?:(\d+)Y)?(?:(\d+)M)?(?:(\d+)([DW]))?(?:T(?:(\d+)H)?(?:(\d+)M)?(?:(\d+(?:\.\d+)?)S)?)?$",
    RegexOptions.Compiled
  );

  /// <summary>
  /// Validates a <see cref="MetricValue"/>
  /// </summary>
  /// <param name="metricValue">The <see cref="MetricValue"/></param>
  /// <param name="mobroService">The <see cref="IMoBroService"/> instance</param>
  /// <exception cref="MetricValueValidationException">In case the value is invalid</exception>
  internal static void Validate(this in MetricValue metricValue, IMoBroService mobroService)
  {
    // check for valid id
    if (!IdValidationRegex.IsMatch(metricValue.Id))
    {
      throw new MetricValueValidationException(metricValue.Id, $"Invalid id for metric value: {metricValue.Id}");
    }

    // check whether metric is registered
    if (!mobroService.TryGet(metricValue.Id, out Metric metric))
    {
      throw new MetricValueValidationException(metricValue.Id,
        $"Unexpected metric value. Metric with id {metricValue.Id} not registered");
    }

    // get ValueType of metric
    MetricValueType valueType;
    if (mobroService.TryGet(metric.TypeId, out MetricType type))
    {
      valueType = type.ValueType;
    }
    else if (Enum.TryParse<CoreMetricType>(metric.TypeId, true, out var coreType))
    {
      valueType = coreType.GetValueType();
    }
    else if (metric.TypeId.Length == 3 && Enum.TryParse<CoreMetricTypeCurrency>(metric.TypeId, true, out _))
    {
      valueType = MetricValueType.Currency;
    }
    else
    {
      throw new MetricValueValidationException(metricValue.Id,
        $"Unknown type '{metric.TypeId}' for metric {metricValue.Id}");
    }

    // no additional validation for null values
    if (metricValue.Value is null) return;

    // specific validation based on value type
    switch (valueType)
    {
      case MetricValueType.Custom:
        ValidateCustom(metricValue);
        break;
      case MetricValueType.String:
        ValidateString(metricValue);
        break;
      case MetricValueType.Numeric:
      case MetricValueType.Currency:
        ValidateNumeric(metricValue);
        break;
      case MetricValueType.Duration:
        ValidateDuration(metricValue);
        break;
      case MetricValueType.DateTime:
        ValidateDateTime(metricValue);
        break;
      case MetricValueType.DateOnly:
        ValidateDateOnly(metricValue);
        break;
      case MetricValueType.TimeOnly:
        ValidateTimeOnly(metricValue);
        break;
      case MetricValueType.Resource:
        ValidateResource(metricValue, mobroService);
        break;
      default:
        throw new MetricValueValidationException(metricValue.Id,
          $"Unknown metric value type '{valueType}' for metric '{metricValue.Id}'");
    }
  }

  private static void ValidateCustom(MetricValue metricValue)
  {
    throw new MetricValueValidationException(metricValue.Id,
      $"Metric values of type '{MetricValueType.Custom}' currently not supported");
  }

  private static void ValidateString(MetricValue metricValue)
  {
    if (metricValue.Value is not string)
    {
      throw InvalidType(MetricValueType.String, metricValue);
    }
  }

  private static void ValidateNumeric(MetricValue metricValue)
  {
    if (!IsNumber(metricValue.Value))
    {
      throw InvalidType(MetricValueType.Numeric, metricValue);
    }
  }

  private static void ValidateDuration(MetricValue metricValue)
  {
    switch (metricValue.Value)
    {
      case TimeSpan:
        break;
      case string str:
        if (!Iso8601DurationRegex.IsMatch(str))
        {
          throw new MetricValueValidationException(metricValue.Id,
            $"Duration value for metric '{metricValue.Id}' does not conform to ISO8601");
        }

        break;
      default:
        throw InvalidType(MetricValueType.Duration, metricValue);
    }
  }

  private static void ValidateDateTime(MetricValue metricValue)
  {
    switch (metricValue.Value)
    {
      case DateTime:
        break;
      case DateTimeOffset:
        break;
      case string str:
        if (!DateTimeOffset.TryParse(str, out _))
        {
          throw new MetricValueValidationException(metricValue.Id,
            $"Value of metric '{metricValue.Id}' can not be parsed to DateTimeOffset: {str}");
        }

        break;
      default:
        throw InvalidType(MetricValueType.DateTime, metricValue);
    }
  }


  private static void ValidateDateOnly(MetricValue metricValue)
  {
    switch (metricValue.Value)
    {
      case DateOnly date:
        break;
      case string str:
        if (!DateOnly.TryParse(str, out _))
        {
          throw new MetricValueValidationException(metricValue.Id,
            $"Value of metric '{metricValue.Id}' can not be parsed to DateOnly: {str}");
        }

        break;
      default:
        throw InvalidType(MetricValueType.DateOnly, metricValue);
    }
  }

  private static void ValidateTimeOnly(MetricValue metricValue)
  {
    switch (metricValue.Value)
    {
      case TimeOnly:
        break;
      case string str:
        if (!TimeOnly.TryParse(str, out _))
        {
          throw new MetricValueValidationException(metricValue.Id,
            $"Value of metric '{metricValue.Id}' can not be parsed to TimeOnly: {str}");
        }

        break;
      default:
        throw InvalidType(MetricValueType.TimeOnly, metricValue);
    }
  }

  private static void ValidateResource(MetricValue metricValue, IMoBroService moBroService)
  {
    if (metricValue.Value is not string stringVal)
    {
      throw InvalidType(MetricValueType.Resource, metricValue);
    }

    if (!moBroService.TryGet<IResource>(stringVal, out _))
    {
      throw new MetricValueValidationException(metricValue.Id,
        $"Invalid value for metric '{metricValue.Id}': Resource with id '{stringVal}' not registered");
    }
  }

  private static bool IsNumber(object? obj) => obj switch
  {
    null => false,
    string str => double.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out _),
    _ => obj is sbyte
      or byte
      or short
      or ushort
      or int
      or uint
      or long
      or ulong
      or float
      or double
      or decimal
  };

  private static MetricValueValidationException InvalidType(MetricValueType type, MetricValue metricValue) => new(
    metricValue.Id,
    $"Invalid value for metric '{metricValue.Id}' of ValueType '{type}': {metricValue.Value}"
  );
}