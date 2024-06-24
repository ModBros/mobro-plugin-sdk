using System;
using System.ComponentModel.DataAnnotations;

namespace MoBro.Plugin.SDK.Models.Metrics;

/// <summary>
/// A unit to present the value of a <see cref="Metric"/> in (e.g. degrees Celsius for temperature).
/// A unit always belongs to a specific <see cref="MetricType"/>.
/// </summary>
public sealed class Unit
{
  /// <summary>
  /// Creates a new unit
  /// </summary>
  /// <param name="label">The label</param>
  /// <param name="abbreviation">The unit abbreviation</param>
  /// <param name="description">The optional textual description</param>
  /// <param name="fromBaseFormula">The formula to derive this unit from the base unit of its <see cref="MetricType"/></param>
  /// <param name="toBaseFormula">The formula to convert this unit back to the base unit of its <see cref="MetricType"/></param>
  public Unit(string label, string abbreviation, string? description, string fromBaseFormula, string toBaseFormula)
  {
    Label = label ?? throw new ArgumentNullException(nameof(label));
    Abbreviation = abbreviation ?? throw new ArgumentNullException(nameof(abbreviation));
    Description = description;
    FromBaseFormula = fromBaseFormula ?? throw new ArgumentNullException(nameof(fromBaseFormula));
    ToBaseFormula = toBaseFormula ?? throw new ArgumentNullException(nameof(toBaseFormula));
  }

  /// <summary>
  /// The name of the unit
  /// </summary>
  [Required]
  [Length(1, 32)]
  public string Label { get; set; }

  /// <summary>
  /// The units abbreviation
  /// </summary>
  [Required]
  [Length(1, 8)]
  public string Abbreviation { get; set; }

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
  public string FromBaseFormula { get; set; }

  /// <summary>
  /// The formula to apply to convert this unit into the base unit of its <see cref="MetricType"/>. (e.g.: x/10)
  /// </summary>
  [Required]
  [Length(1, 64)]
  public string ToBaseFormula { get; set; }
}