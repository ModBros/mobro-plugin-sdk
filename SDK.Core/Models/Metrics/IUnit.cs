namespace MoBro.Plugin.SDK.Models.Metrics;

/// <summary>
/// A unit to present the value of a <see cref="IMetric"/> in (e.g. degrees Celsius for temperature).
/// A unit always belongs to a specific <see cref="IMetricType"/>.
/// </summary>
public interface IUnit
{
  /// <summary>
  /// The name of the unit
  /// </summary>
  public string Label { get; }

  /// <summary>
  /// The units abbreviation
  /// </summary>
  public string Abbreviation { get; }

  /// <summary>
  /// An optional textual description
  /// </summary>
  public string? Description { get; }

  /// <summary>
  /// The formula to apply to derive this unit from the base unit of its <see cref="IMetricType"/>. (e.g.: x*10)
  /// </summary>
  public string FromBaseFormula { get; }

  /// <summary>
  /// The formula to apply to convert this unit into the base unit of its <see cref="IMetricType"/>. (e.g.: x/10)
  /// </summary>
  public string ToBaseFormula { get; }
}