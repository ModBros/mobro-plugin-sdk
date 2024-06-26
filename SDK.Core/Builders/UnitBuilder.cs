using System;
using MoBro.Plugin.SDK.Models.Metrics;

namespace MoBro.Plugin.SDK.Builders;

/// <summary>
/// Builder to create a new <see cref="Unit"/>
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
    ArgumentNullException.ThrowIfNull(fromBaseFormula);
    ArgumentNullException.ThrowIfNull(toBaseFormula);
    _fromBaseFormula = fromBaseFormula;
    _toBaseFormula = toBaseFormula;
    return this;
  }

  /// <inheritdoc />
  public IAbbreviationStage WithLabel(string label, string? description = null)
  {
    ArgumentNullException.ThrowIfNull(label);
    _label = label;
    _description = description;
    return this;
  }

  /// <inheritdoc />
  public IBuildStage WithAbbreviation(string abbreviation)
  {
    ArgumentNullException.ThrowIfNull(abbreviation);
    _abbreviation = abbreviation;
    return this;
  }

  /// <inheritdoc />
  public Unit Build()
  {
    return new Unit
    {
      Label = _label ?? throw new ArgumentNullException(nameof(_label)),
      Abbreviation = _abbreviation ?? throw new ArgumentNullException(nameof(_abbreviation)),
      Description = _description,
      FromBaseFormula = _fromBaseFormula ?? throw new ArgumentNullException(nameof(_fromBaseFormula)),
      ToBaseFormula = _toBaseFormula ?? throw new ArgumentNullException(nameof(_toBaseFormula))
    };
  }

  /// <summary>
  /// Building stage of the <see cref="UnitBuilder"/>
  /// </summary>
  public interface IDerivedUnitStage
  {
    /// <summary>
    /// Sets the formulas to derive (and convert back) this unit from the base unit of its
    /// <see cref="MetricType"/>
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
    /// Sets the label and optionally a description of the <see cref="Unit"/>
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
    /// Sets the abbreviation for the <see cref="Unit"/>
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
    /// Builds and creates the <see cref="Unit"/>
    /// </summary>
    /// <returns>The <see cref="Unit"/></returns>
    public Unit Build();
  }
}