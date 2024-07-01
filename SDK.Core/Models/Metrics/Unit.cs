using System.ComponentModel.DataAnnotations;

namespace MoBro.Plugin.SDK.Models.Metrics;

/// <summary>
/// A unit to present the value of a <see cref="Metric"/> in (e.g. degrees Celsius for temperature).
/// A unit always belongs to a specific <see cref="MetricType"/>.
/// </summary>
public sealed class Unit
{
  /// <summary>
  /// The name of the unit
  /// </summary>
  [Required]
  [Length(1, 64)]
  public required string Label { get; set; }

  /// <summary>
  /// The units abbreviation
  /// </summary>
  [Required]
  [Length(1, 64)]
  public required string Abbreviation { get; set; }

  /// <summary>
  /// An optional textual description
  /// </summary>
  [MaxLength(256)]
  public string? Description { get; set; }

  /// <summary>
  /// The formula to apply to derive this unit from the base unit of its <see cref="MetricType"/>. (e.g.: x*10)
  /// </summary>
  [Required]
  [Length(1, 64)]
  public required string FromBaseFormula { get; set; }

  /// <summary>
  /// The formula to apply to convert this unit into the base unit of its <see cref="MetricType"/>. (e.g.: x/10)
  /// </summary>
  [Required]
  [Length(1, 64)]
  public required string ToBaseFormula { get; set; }
}