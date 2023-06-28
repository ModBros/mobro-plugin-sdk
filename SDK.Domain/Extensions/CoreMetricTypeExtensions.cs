using MoBro.Plugin.SDK.Enums;

namespace MoBro.Plugin.SDK.Extensions;

/// <summary>
/// Extension functions for <see cref="CoreMetricType"/> 
/// </summary>
public static class CoreMetricTypeExtensions
{
  /// <summary>
  /// Get the associated <see cref="MetricValueType"/> for a given <see cref="CoreMetricType"/>
  /// </summary>
  /// <param name="type">The <see cref="CoreMetricType"/></param>
  /// <returns>The associated <see cref="MetricValueType"/></returns>
  public static MetricValueType GetValueType(this CoreMetricType type)
  {
    switch (type)
    {
      case CoreMetricType.DataFlow:
      case CoreMetricType.ElectricCurrent:
      case CoreMetricType.ElectricPotential:
      case CoreMetricType.ElectricResistance:
      case CoreMetricType.Data:
      case CoreMetricType.Frequency:
      case CoreMetricType.Multiplier:
      case CoreMetricType.Numeric:
      case CoreMetricType.Power:
      case CoreMetricType.Rotation:
      case CoreMetricType.Temperature:
      case CoreMetricType.Usage:
      case CoreMetricType.Pressure:
      case CoreMetricType.Volume:
      case CoreMetricType.VolumeFlow:
      case CoreMetricType.Length:
      case CoreMetricType.Speed:
      case CoreMetricType.Mass:
      case CoreMetricType.Area:
      case CoreMetricType.Angle:
        return MetricValueType.Numeric;
      case CoreMetricType.Text:
        return MetricValueType.String;
      case CoreMetricType.Duration:
        return MetricValueType.Duration;
      case CoreMetricType.DateTime:
        return MetricValueType.DateTime;
      case CoreMetricType.Date:
        return MetricValueType.DateOnly;
      case CoreMetricType.Time:
        return MetricValueType.TimeOnly;
      case CoreMetricType.Image:
      case CoreMetricType.Icon:
        return MetricValueType.Resource;
      default:
        return MetricValueType.Custom;
    }
  }
}