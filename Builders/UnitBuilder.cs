using System;
using MoBro.Plugin.SDK.Models.Metrics;

namespace MoBro.Plugin.SDK.Builders;

/// <summary>
/// Builder to create a new <see cref="IUnit"/>
/// </summary>
public sealed class UnitBuilder :
  UnitBuilder.IDerivedUnitStage,
  UnitBuilder.ILabelStage,
  UnitBuilder.IAbbreviationStage,
  UnitBuilder.IBuildStage
{
  private string? _label;
  private string? _abbreviation;
  private string? _description;
  private string _fromBaseFormula = "x";
  private string _toBaseFormula = "x";

  private UnitBuilder()
  {
  }

  internal static ILabelStage CreateBaseUnit()
  {
    return new UnitBuilder();
  }

  internal static IDerivedUnitStage CreateUnit()
  {
    return new UnitBuilder();
  }

  /// <inheritdoc />
  public ILabelStage WithConversionFormula(string fromBaseFormula, string toBaseFormula)
  {
    _fromBaseFormula = fromBaseFormula;
    _toBaseFormula = toBaseFormula;
    return this;
  }

  /// <inheritdoc />
  public IAbbreviationStage WithLabel(string label, string? description = null)
  {
    _label = label;
    _description = description;
    return this;
  }

  /// <inheritdoc />
  public IBuildStage WithAbbreviation(string abbreviation)
  {
    _abbreviation = abbreviation;
    return this;
  }

  /// <inheritdoc />
  public IUnit Build()
  {
    return new Unit(
      _label ?? throw new InvalidOperationException("Unit label must not be null"),
      _abbreviation ?? throw new InvalidOperationException("Unit abbreviation must not be null"),
      _description,
      _fromBaseFormula ?? throw new InvalidOperationException("FromBaseFormula must not be null"),
      _toBaseFormula ?? throw new InvalidOperationException("ToBaseFormula must not be null")
    );
  }

  /// <summary>
  /// Building stage of the <see cref="UnitBuilder"/>
  /// </summary>
  public interface IDerivedUnitStage
  {
    /// <summary>
    /// Sets the formulas to derive (and convert back) this unit from the base unit of its
    /// <see cref="IMetricType"/>
    /// </summary>
    /// <param name="fromBaseFormula">The formula to derive the unit from the base unit</param>
    /// <param name="toBaseFormula">The formula to convert this unit back into the base unit</param>
    /// <returns>The next building stage</returns>
    public ILabelStage WithConversionFormula(string fromBaseFormula, string toBaseFormula);
  }

  /// <summary>
  /// Building stage of the <see cref="UnitBuilder"/>
  /// </summary>
  public interface ILabelStage
  {
    /// <summary>
    /// Sets the label and optionally a description of the <see cref="IUnit"/>
    /// </summary>
    /// <param name="label">The label</param>
    /// <param name="description">The optional textual description</param>
    /// <returns>The next building stage</returns>
    public IAbbreviationStage WithLabel(string label, string? description = null);
  }

  /// <summary>
  /// Building stage of the <see cref="UnitBuilder"/>
  /// </summary>
  public interface IAbbreviationStage
  {
    /// <summary>
    /// Sets the abbreviation for the <see cref="IUnit"/>
    /// </summary>
    /// <param name="abbreviation">The abbreviation</param>
    /// <returns>The next building stage</returns>
    public IBuildStage WithAbbreviation(string abbreviation);
  }

  /// <summary>
  /// Building stage of the <see cref="UnitBuilder"/>
  /// </summary>
  public interface IBuildStage
  {
    /// <summary>
    /// Builds and creates the <see cref="IUnit"/>
    /// </summary>
    /// <returns>The <see cref="IUnit"/></returns>
    public IUnit Build();
  }
}