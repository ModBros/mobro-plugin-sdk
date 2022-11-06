using System;

namespace MoBro.Plugin.SDK.Models.Metrics;

/// <summary>
/// The actual value of a <see cref="IMetric"/>.
/// The value must conform to the <see cref="IMetricType"/> of the Metric this value is from.
/// </summary>
public interface IMetricValue : IMoBroItem
{
  /// <summary>
  /// The date and time the value was recorded or measured at
  /// </summary>
  public DateTime Timestamp { get; }

  /// <summary>
  /// The actual value
  /// </summary>
  public object? Value { get; }
}