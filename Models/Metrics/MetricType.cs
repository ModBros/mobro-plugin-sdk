using System;
using System.Collections.Generic;
using System.Linq;
using MoBro.Plugin.SDK.Builders;
using MoBro.Plugin.SDK.Enums;

namespace MoBro.Plugin.SDK.Models.Metrics;

/// <inheritdoc />
public sealed class MetricType : IMetricType
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

  /// <inheritdoc />
  public string Label { get; set; }

  /// <inheritdoc />
  public MetricValueType ValueType { get; set; }

  /// <inheritdoc />
  public string? Description { get; set; }

  /// <inheritdoc />
  public string? Icon { get; set; }

  /// <inheritdoc />
  public IUnit? BaseUnit { get; set; }

  /// <inheritdoc />
  public IEnumerable<IUnit>? Units { get; set; } = Enumerable.Empty<IUnit>();
}