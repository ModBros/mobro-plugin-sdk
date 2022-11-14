using System;

namespace MoBro.Plugin.SDK.Models.Metrics;

/// <summary>
/// The actual value of a <see cref="IMetric"/>.
/// The value must conform to the <see cref="IMetricType"/> of the Metric this value is from.
/// </summary>
public readonly record struct MetricValue
{
  /// <summary>
  /// Creates a new metric value
  /// </summary>
  /// <param name="id">The id of the metric this value belongs to</param>
  /// <param name="timestamp">The date and time the value was recorded or measured at</param>
  /// <param name="value">The value</param>
  public MetricValue(string id, DateTime timestamp, object? value)
  {
    Id = id ?? throw new ArgumentNullException(nameof(id));
    Timestamp = timestamp;
    Value = value;
  }

  /// <summary>
  /// The id of the metric
  /// </summary>
  public string Id { get; }

  /// <summary>
  /// The date and time the value was recorded or measured at
  /// </summary>
  public DateTime Timestamp { get; }

  /// <summary>
  /// The actual value
  /// </summary>
  public object? Value { get; }
}