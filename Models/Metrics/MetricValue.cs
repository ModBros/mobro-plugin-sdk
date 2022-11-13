using System;

namespace MoBro.Plugin.SDK.Models.Metrics;

/// <inheritdoc />
public sealed class MetricValue : IMetricValue
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

  /// <inheritdoc />
  public string Id { get; set; }

  /// <inheritdoc />
  public DateTime Timestamp { get; set; }

  /// <inheritdoc />
  public object? Value { get; set; }
}