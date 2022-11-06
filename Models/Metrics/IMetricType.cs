using System.Collections.Generic;
using MoBro.Plugin.SDK.Enums;

namespace MoBro.Plugin.SDK.Models.Metrics;

/// <summary>
/// The type of a <see cref="IMetric"/>.
/// Defines the <see cref="MetricValueType"/> the metrics value must conform to and the applicable
/// <see cref="IUnit"/>s.
/// </summary>
public interface IMetricType : IMoBroItem
{
  /// <summary>
  /// The textual name of the type
  /// </summary>
  public string Label { get; }

  /// <summary>
  /// The <see cref="MetricValueType"/> a value of this type must conform to
  /// </summary>
  public MetricValueType ValueType { get; }

  /// <summary>
  /// An optional textual description
  /// </summary>
  public string? Description { get; }

  /// <summary>
  /// An optional icon id
  /// </summary>
  public string? Icon { get; }

  /// <summary>
  /// The optional base <see cref="IUnit"/> in which values of this type must be returned in
  /// </summary>
  public IUnit? BaseUnit { get; }

  /// <summary>
  /// A list of <see cref="IUnit"/>s the <see cref="BaseUnit"/> can be converted to.
  /// </summary>
  public IEnumerable<IUnit>? Units { get; }
}