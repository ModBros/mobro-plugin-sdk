using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MoBro.Plugin.SDK.Enums;

namespace MoBro.Plugin.SDK.Models.Metrics;

/// <summary>
/// The type of <see cref="Metric"/>.
/// Defines the <see cref="MetricValueType"/> the metrics value must conform to and the applicable
/// <see cref="Unit"/>s.
/// </summary>
public sealed class MetricType : IMoBroItem
{
  /// <inheritdoc />
  public required string Id { get; set; }

  /// <summary>
  /// The textual name of the type
  /// </summary>
  [Required]
  [Length(1, 32)]
  public required string Label { get; set; }

  /// <summary>
  /// The <see cref="MetricValueType"/> a value of this type must conform to
  /// </summary>
  [Required]
  public MetricValueType ValueType { get; set; }

  /// <summary>
  /// An optional textual description
  /// </summary>
  [MaxLength(256)]
  public string? Description { get; set; }

  /// <summary>
  /// An optional icon id
  /// </summary>
  [Length(1, 128)]
  public string? Icon { get; set; }

  /// <summary>
  /// The optional base <see cref="Unit"/> in which values of this type must be returned in
  /// </summary>
  public Unit? BaseUnit { get; set; }

  /// <summary>
  /// A list of <see cref="Unit"/>s the <see cref="BaseUnit"/> can be converted to.
  /// </summary>
  [MaxLength(32)]
  public IEnumerable<Unit>? Units { get; set; } = new List<Unit>();
}