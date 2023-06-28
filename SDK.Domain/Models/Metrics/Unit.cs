using System;

namespace MoBro.Plugin.SDK.Models.Metrics;

/// <inheritdoc />
public sealed class Unit : IUnit
{
  /// <summary>
  /// Creates a new unit
  /// </summary>
  /// <param name="label">The label</param>
  /// <param name="abbreviation">The unit abbreviation</param>
  /// <param name="description">The optional textual description</param>
  /// <param name="fromBaseFormula">The formula to derive this unit from the base unit of its <see cref="IMetricType"/></param>
  /// <param name="toBaseFormula">The formula to convert this unit back to the base unit of its <see cref="IMetricType"/></param>
  public Unit(string label, string abbreviation, string? description, string fromBaseFormula, string toBaseFormula)
  {
    Label = label ?? throw new ArgumentNullException(nameof(label));
    Abbreviation = abbreviation ?? throw new ArgumentNullException(nameof(abbreviation));
    Description = description;
    FromBaseFormula = fromBaseFormula ?? throw new ArgumentNullException(nameof(fromBaseFormula));
    ToBaseFormula = toBaseFormula ?? throw new ArgumentNullException(nameof(toBaseFormula));
  }

  /// <inheritdoc />
  public string Label { get; set; }

  /// <inheritdoc />
  public string Abbreviation { get; set; }

  /// <inheritdoc />
  public string? Description { get; set; }

  /// <inheritdoc />
  public string FromBaseFormula { get; set; }

  /// <inheritdoc />
  public string ToBaseFormula { get; set; }
}