﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MoBro.Plugin.SDK.Models.Metrics;

/// <summary>
/// The actual value of a <see cref="Metric"/>.
/// The value must conform to the <see cref="MetricType"/> of the <see cref="Metric"/> this value belongs to.
/// </summary>
public readonly record struct MetricValue
{
  /// <summary>
  /// Creates a new metric value
  /// </summary>
  /// <param name="id">The id of the metric this value belongs to</param>
  /// <param name="timestamp">The date and time the value was recorded or measured at in UTC</param>
  /// <param name="value">The new value of the metric</param>
  public MetricValue(string id, DateTime timestamp, object? value)
  {
    Id = id ?? throw new ArgumentNullException(nameof(id));
    Timestamp = timestamp;
    Value = value;
  }

  /// <summary>
  /// Creates a new metric value
  /// </summary>
  /// <param name="id">The id of the metric this value belongs to</param>
  /// <param name="timestamp">The date and time the value was recorded or measured at</param>
  /// <param name="value">The new value of the metric</param>
  public MetricValue(string id, DateTimeOffset timestamp, object? value)
  {
    Id = id ?? throw new ArgumentNullException(nameof(id));
    Timestamp = timestamp.UtcDateTime;
    Value = value;
  }

  /// <summary>
  /// Creates a new metric value with the timestamp automatically set to <see cref="DateTime.UtcNow"/>
  /// </summary>
  /// <param name="id">The id of the metric this value belongs to</param>
  /// <param name="value">The new value of the metric</param>
  public MetricValue(string id, object? value)
  {
    Id = id ?? throw new ArgumentNullException(nameof(id));
    Timestamp = DateTime.UtcNow;
    Value = value;
  }

  /// <summary>
  /// The id of the metric
  /// </summary>
  [Required]
  [Length(1, 256)]
  [RegularExpression(@"^[\w\.\-]+$")]
  public string Id { get; init; }

  /// <summary>
  /// The date and time the value was recorded or measured at in UTC
  /// </summary>
  [Required]
  public DateTime Timestamp { get; init; } = DateTime.UtcNow;

  /// <summary>
  /// The actual value
  /// </summary>
  public object? Value { get; init; }
}