using System;
using System.Collections.Generic;
using MoBro.Plugin.SDK.Builders;
using MoBro.Plugin.SDK.Enums;

namespace MoBro.Plugin.SDK.Models.Metrics;

/// <summary>
/// The type of a <see cref="Metric"/>.
/// Defines the <see cref="MetricValueType"/> the metrics value must conform to and the applicable
/// <see cref="Unit"/>s.
/// </summary>
public sealed class MetricType : IMoBroItem 
{
  /// <summary>
  /// Creates a new metric type.
  /// See also <see cref="MoBroItem"/> for a guided builder.
  /// </summary>
  /// <param name="id">The id (must be unique within the scope of the plugin)</param>
  /// <param name="label">The label</param>
  /// <param name="valueType">The <see cref="MetricValueType"/></param>
  public MetricType(string id, string label, MetricValueType valueType)
  {
    Id = id ?? throw new ArgumentNullException(nameof(id));
    Label = label ?? throw new ArgumentNullException(nameof(label));
    ValueType = valueType;
  }

  /// <inheritdoc />
  public string Id { get; set; }

  /// <summary>
  /// The textual name of the type
  /// </summary>
  public string Label { get; set; }

  /// <summary>
  /// The <see cref="MetricValueType"/> a value of this type must conform to
  /// </summary>
  public MetricValueType ValueType { get; set; }

  /// <summary>
  /// An optional textual description
  /// </summary>
  public string? Description { get; set; }

  /// <summary>
  /// An optional icon id
  /// </summary>
  public string? Icon { get; set; }

  /// <summary>
  /// The optional base <see cref="Unit"/> in which values of this type must be returned in
  /// </summary>
  public Unit? BaseUnit { get; set; }

  /// <summary>
  /// A list of <see cref="Unit"/>s the <see cref="BaseUnit"/> can be converted to.
  /// </summary>
  public IEnumerable<Unit>? Units { get; set; } = new List<Unit>();
}