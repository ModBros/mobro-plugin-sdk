using System;
using System.Collections.Generic;
using MoBro.Plugin.SDK.Models.Settings;

namespace MoBro.Plugin.SDK.Builders;

/// <summary>
/// Builder to create a new <see cref="SettingsFieldBase"/>
/// </summary>
public sealed class SettingsBuilder :
  SettingsBuilder.INameStage,
  SettingsBuilder.ILabelStage,
  SettingsBuilder.IBuildStage,
  SettingsBuilder.ITypeNumericStage,
  SettingsBuilder.ITypeStringStage,
  SettingsBuilder.ITypeCheckboxStage,
  SettingsBuilder.ITypeSelectStage
{
  // base properties
  private string? _name;
  private string? _label;
  private string? _description;
  private bool _required;
  private object? _defaultValue;

  // specific properties for numeric
  private double? _numericMin;
  private double? _numericMax;

  // specific properties for string
  private string? _stringRegex;

  // specific properties for select
  private List<SettingsFieldSelectOption>? _selectOptions;

  private SettingsBuilder()
  {
  }

  internal static INameStage CreateSetting() => new SettingsBuilder();

  /// <inheritdoc />
  public ILabelStage WithName(string name)
  {
    ArgumentNullException.ThrowIfNull(name);
    _name = name;
    return this;
  }

  /// <inheritdoc />
  public IBuildStage WithLabel(string label, string? description = null)
  {
    ArgumentNullException.ThrowIfNull(label);
    _label = label;
    _description = description;
    return this;
  }

  /// <inheritdoc />
  public IBuildStage AsRequired()
  {
    _required = true;
    return this;
  }

  /// <inheritdoc />
  public IBuildStage Required(bool required)
  {
    _required = required;
    return this;
  }

  /// <inheritdoc />
  public ITypeNumericStage OfTypeNumber() => this;

  /// <inheritdoc />
  public ITypeCheckboxStage OfTypeCheckbox() => this;

  /// <inheritdoc />
  public ITypeStringStage OfTypeString() => this;

  /// <inheritdoc />
  public ITypeSelectStage OfTypeSelect() => this;

  /// <inheritdoc />
  public ITypeNumericStage WithDefault(double? defaultValue)
  {
    _defaultValue = defaultValue;
    return this;
  }

  /// <inheritdoc />
  public ITypeNumericStage WithMin(double? minValue)
  {
    _numericMin = minValue;
    return this;
  }

  /// <inheritdoc />
  public ITypeNumericStage WithMax(double? maxValue)
  {
    _numericMax = maxValue;
    return this;
  }

  /// <inheritdoc />
  SettingsFieldNumber ITypeNumericStage.Build()
  {
    return new SettingsFieldNumber(
      _name ?? throw new ArgumentNullException(nameof(_name)),
      _label ?? throw new ArgumentNullException(nameof(_label)),
      _description,
      _required
    )
    {
      DefaultValue = (double?)_defaultValue,
      Min = _numericMin,
      Max = _numericMax
    };
  }

  /// <inheritdoc />
  ITypeStringStage ITypeStringStage.WithDefault(string? defaultValue)
  {
    _defaultValue = defaultValue;
    return this;
  }

  /// <inheritdoc />
  public ITypeStringStage WithRegex(string? regex)
  {
    _stringRegex = regex;
    return this;
  }

  /// <inheritdoc />
  SettingsFieldString ITypeStringStage.Build()
  {
    return new SettingsFieldString(
      _name ?? throw new ArgumentNullException(nameof(_name)),
      _label ?? throw new ArgumentNullException(nameof(_label)),
      _description,
      _required
    )
    {
      DefaultValue = (string?)_defaultValue,
      Regex = _stringRegex
    };
  }

  /// <inheritdoc />
  public ITypeCheckboxStage WithDefault(bool? defaultValue)
  {
    _defaultValue = defaultValue;
    return this;
  }

  /// <inheritdoc />
  public SettingsFieldCheckbox Build()
  {
    return new SettingsFieldCheckbox(
      _name ?? throw new ArgumentNullException(nameof(_name)),
      _label ?? throw new ArgumentNullException(nameof(_label)),
      _description,
      _required
    )
    {
      DefaultValue = (bool?)_defaultValue,
    };
  }

  /// <inheritdoc />
  ITypeSelectStage ITypeSelectStage.WithDefault(string? defaultValue)
  {
    _defaultValue = defaultValue;
    return this;
  }

  /// <inheritdoc />
  public ITypeSelectStage WithOption(string label, string value)
  {
    _selectOptions ??= new List<SettingsFieldSelectOption>();
    _selectOptions.Add(new SettingsFieldSelectOption(
      label ?? throw new ArgumentNullException(nameof(label)),
      value ?? throw new ArgumentNullException(nameof(value))
    ));
    return this;
  }

  /// <inheritdoc />
  SettingsFieldSelect ITypeSelectStage.Build()
  {
    return new SettingsFieldSelect(
      _name ?? throw new ArgumentNullException(nameof(_name)),
      _label ?? throw new ArgumentNullException(nameof(_label)),
      _description,
      _required
    )
    {
      DefaultValue = (string?)_defaultValue,
      Options = _selectOptions ?? new List<SettingsFieldSelectOption>(0)
    };
  }

  /// <summary>
  /// Building stage of the <see cref="SettingsBuilder"/>
  /// </summary>
  public interface INameStage
  {
    /// <summary>
    /// Sets the name of the setting
    /// </summary>
    /// <param name="name">The settings name</param>
    /// <returns>The next building stage</returns>
    public ILabelStage WithName(string name);
  }

  /// <summary>
  /// Building stage of the <see cref="SettingsBuilder"/>
  /// </summary>
  public interface ILabelStage
  {
    /// <summary>
    /// Sets the label and optionally a description of the setting
    /// </summary>
    /// <param name="label">The label</param>
    /// <param name="description">The optional textual description</param>
    /// <returns>The next building stage</returns>
    public IBuildStage WithLabel(string label, string? description = null);
  }

  /// <summary>
  /// Building stage of the <see cref="SettingsBuilder"/>
  /// </summary>
  public interface IBuildStage
  {
    /// <summary>
    /// Marks the setting as required
    /// </summary>
    /// <returns>The next building stage</returns>
    public IBuildStage AsRequired();

    /// <summary>
    /// Sets whether this setting is required or not
    /// </summary>
    /// <param name="required">The required flag</param>
    /// <returns>The next building stage</returns>
    public IBuildStage Required(bool required);

    /// <summary>
    /// Continue building a <see cref="SettingsFieldNumber"/> 
    /// </summary>
    /// <returns>The next building stage</returns>
    public ITypeNumericStage OfTypeNumber();

    /// <summary>
    /// Continue building a <see cref="SettingsFieldCheckbox"/> 
    /// </summary>
    /// <returns>The next building stage</returns>
    public ITypeCheckboxStage OfTypeCheckbox();

    /// <summary>
    /// Continue building a <see cref="SettingsFieldString"/> 
    /// </summary>
    /// <returns>The next building stage</returns>
    public ITypeStringStage OfTypeString();

    /// <summary>
    /// Continue building a <see cref="SettingsFieldSelect"/> 
    /// </summary>
    /// <returns>The next building stage</returns>
    public ITypeSelectStage OfTypeSelect();
  }

  /// <summary>
  /// Building stage of the <see cref="SettingsBuilder"/>
  /// </summary>
  public interface ITypeNumericStage
  {
    /// <summary>
    /// The default value the setting
    /// </summary>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The building stage</returns>
    public ITypeNumericStage WithDefault(double? defaultValue);

    /// <summary>
    /// Set a minimum value for this setting for validation
    /// </summary>
    /// <param name="minValue">The minimum value</param>
    /// <returns>The building stage</returns>
    public ITypeNumericStage WithMin(double? minValue);

    /// <summary>
    /// Set a maximum value for this setting for validation
    /// </summary>
    /// <param name="maxValue">The maximum value</param>
    /// <returns>The building stage</returns>
    public ITypeNumericStage WithMax(double? maxValue);

    /// <summary>
    /// Builds and creates the <see cref="SettingsFieldNumber"/>
    /// </summary>
    /// <returns>The <see cref="SettingsFieldNumber"/></returns>
    public SettingsFieldNumber Build();
  }

  /// <summary>
  /// Building stage of the <see cref="SettingsBuilder"/>
  /// </summary>
  public interface ITypeSelectStage
  {
    /// <summary>
    /// The default value the setting
    /// </summary>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The building stage</returns>
    public ITypeSelectStage WithDefault(string? defaultValue);

    /// <summary>
    /// Adds an option to the selection
    /// </summary>
    /// <param name="label">The label of the option</param>
    /// <param name="value">The value of the option</param>
    /// <returns>The building stage</returns>
    public ITypeSelectStage WithOption(string label, string value);

    /// <summary>
    /// Builds and creates the <see cref="SettingsFieldSelect"/>
    /// </summary>
    /// <returns>The <see cref="SettingsFieldSelect"/></returns>
    public SettingsFieldSelect Build();
  }

  /// <summary>
  /// Building stage of the <see cref="SettingsBuilder"/>
  /// </summary>
  public interface ITypeStringStage
  {
    /// <summary>
    /// The default value the setting
    /// </summary>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The building stage</returns>
    public ITypeStringStage WithDefault(string? defaultValue);

    /// <summary>
    /// Set a regex for input validation
    /// </summary>
    /// <param name="regex">The regex</param>
    /// <returns>The building stage</returns>
    public ITypeStringStage WithRegex(string? regex);

    /// <summary>
    /// Builds and creates the <see cref="SettingsFieldString"/>
    /// </summary>
    /// <returns>The <see cref="SettingsFieldString"/></returns>
    public SettingsFieldString Build();
  }

  /// <summary>
  /// Building stage of the <see cref="SettingsBuilder"/>
  /// </summary>
  public interface ITypeCheckboxStage
  {
    /// <summary>
    /// The default value the setting
    /// </summary>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The building stage</returns>
    public ITypeCheckboxStage WithDefault(bool? defaultValue);

    /// <summary>
    /// Builds and creates the <see cref="SettingsFieldCheckbox"/>
    /// </summary>
    /// <returns>The <see cref="SettingsFieldCheckbox"/></returns>
    public SettingsFieldCheckbox Build();
  }
}