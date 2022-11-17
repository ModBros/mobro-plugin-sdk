using System;
using MoBro.Plugin.SDK.Enums;

namespace MoBro.Plugin.SDK.Models.Metrics;

/// <summary>
/// A metric represents a single data value provided by a plugin that will be monitored by the service.
/// A metric must be of a registered <see cref="IMetricType"/> and its value must conform to the
/// <see cref="MetricValueType"/> defined in that type.
/// A metric must be assigned to a registered <see cref="ICategory"/>
/// </summary>
public interface IMetric : IMoBroItem
{
  /// <summary>
  /// The textual name of the metric
  /// </summary>
  public string Label { get; }

  /// <summary>
  /// The type of this metric (id of a registered <see cref="IMetricType"/>)
  /// </summary>
  public string TypeId { get; }

  /// <summary>
  /// The category this metric is assigned to (id of a registered <see cref="ICategory"/>)
  /// </summary>
  public string CategoryId { get; }

  /// <summary>
  /// Whether this metric is static or not. (static = the value is fixed and will not change)
  /// </summary>
  public bool IsStatic { get; }

  /// <summary>
  /// An optional textual description
  /// </summary>
  public string? Description { get; }

  /// <summary>
  /// An optional group this metric is part of (id of a registered <see cref="IGroup"/>)
  /// </summary>
  public string? GroupId { get; }
}