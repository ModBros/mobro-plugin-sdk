using System;
using System.Collections.Generic;
using MoBro.Plugin.SDK.Enums;
using MoBro.Plugin.SDK.Models.Metrics;

namespace MoBro.Plugin.SDK.Builders;

/// <summary>
/// Builder to create a new <see cref="MetricType"/>
/// </summary>
public sealed class TypeBuilder :
  TypeBuilder.IIdStage,
  TypeBuilder.ILabelStage,
  TypeBuilder.ITypeStage,
  TypeBuilder.IBuildStage
{
  private string? _id;
  private string? _label;
  private MetricValueType? _valueType;
  private string? _description;
  private string? _icon;
  private Unit? _baseUnit;
  private List<Unit> _units = new();

  private TypeBuilder()
  {
  }

  internal static IIdStage CreateMetricType()
  {
    return new TypeBuilder();
  }

  /// <inheritdoc />
  public ILabelStage WithId(string id)
  {
    ArgumentNullException.ThrowIfNull(id);
    _id = id;
    return this;
  }

  /// <inheritdoc />
  public ITypeStage WithLabel(string label, string? description = null)
  {
    ArgumentNullException.ThrowIfNull(label);
    _label = label;
    _description = description;
    return this;
  }

  /// <inheritdoc />
  public IBuildStage WithIcon(string? iconId)
  {
    _icon = iconId;
    return this;
  }

  /// <inheritdoc />
  public IBuildStage OfValueType(MetricValueType type)
  {
    _valueType = type;
    return this;
  }

  /// <inheritdoc />
  public IBuildStage WithBaseUnit(Func<UnitBuilder.ILabelStage, Unit> builderFunction)
  {
    ArgumentNullException.ThrowIfNull(builderFunction);
    _baseUnit = builderFunction.Invoke(UnitBuilder.CreateBaseUnit());
    return this;
  }

  /// <inheritdoc />
  public IBuildStage WithDerivedUnit(Func<UnitBuilder.IDerivedUnitStage, Unit> builderFunction)
  {
    ArgumentNullException.ThrowIfNull(builderFunction);
    _units.Add(builderFunction.Invoke(UnitBuilder.CreateUnit()));
    return this;
  }

  /// <inheritdoc cref="IBuildStage.Build" />
  public MetricType Build()
  {
    return new MetricType
    {
      Id = _id ?? throw new ArgumentNullException(nameof(_id)),
      Label = _label ?? throw new ArgumentNullException(nameof(_label)),
      ValueType = _valueType ?? throw new ArgumentNullException(nameof(_valueType)),
      Description = _description,
      Icon = _icon,
      BaseUnit = _baseUnit,
      Units = _units
    };
  }

  /// <summary>
  /// Building stage of the <see cref="TypeBuilder"/>
  /// </summary>
  public interface IIdStage
  {
    /// <summary>
    /// Sets the id of the <see cref="MetricType"/>
    /// </summary>
    /// <param name="id">The id (must be unique within the scope of the plugin)</param>
    /// <returns>The next building stage</returns>
    public ILabelStage WithId(string id);
  }

  /// <summary>
  /// Building stage of the <see cref="TypeBuilder"/>
  /// </summary>
  public interface ILabelStage
  {
    /// <summary>
    /// Sets the label and optionally a description of the <see cref="MetricType"/>
    /// </summary>
    /// <param name="label">The label</param>
    /// <param name="description">The optional textual description</param>
    /// <returns>The next building stage</returns>
    public ITypeStage WithLabel(string label, string? description = null);
  }

  /// <summary>
  /// Building stage of the <see cref="TypeBuilder"/>
  /// </summary>
  public interface ITypeStage
  {
    /// <summary>
    /// Sets the <see cref="MetricValueType"/> of this
    /// <see cref="MetricType"/>
    /// </summary>
    /// <param name="type">The <see cref="MetricValueType"/></param>
    /// <returns>The next building stage</returns>
    public IBuildStage OfValueType(MetricValueType type);
  }

  /// <summary>
  /// Building stage of the <see cref="TypeBuilder"/>
  /// </summary>
  public interface IBuildStage
  {
    /// <summary>
    /// Sets the icon of the <see cref="MetricType"/>
    /// </summary>
    /// <param name="iconId">The icon id</param>
    /// <returns>The next building stage</returns>
    public IBuildStage WithIcon(string? iconId);

    /// <summary>
    /// Builds and sets the base <see cref="Unit"/>
    /// for this <see cref="MetricType"/>
    /// </summary>
    /// <param name="builderFunction">The builder function for the <see cref="Unit"/></param>
    /// <returns>The next building stage</returns>
    public IBuildStage WithBaseUnit(Func<UnitBuilder.ILabelStage, Unit> builderFunction);

    /// <summary>
    /// Adds an additional <see cref="Unit"/> to the
    /// <see cref="MetricType"/>. This unit has to be derivable by formula from the
    /// previously defined base unit.
    /// </summary>
    /// <param name="builderFunction">The builder function for the <see cref="Unit"/></param>
    /// <returns>The building stage</returns>
    public IBuildStage WithDerivedUnit(Func<UnitBuilder.IDerivedUnitStage, Unit> builderFunction);

    /// <summary>
    /// Builds and creates the <see cref="MetricType"/>
    /// </summary>
    /// <returns>The <see cref="MetricType"/></returns>
    public MetricType Build();
  }
}