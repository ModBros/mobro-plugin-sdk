using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoBro.Plugin.SDK.Models.Settings;

/// <summary>
/// A select settings field
/// </summary>
public class SettingsFieldSelect : SettingsFieldBase
{
  /// <summary>
  /// The default value of the field
  /// </summary>
  public string? Default { get; set; }

  /// <summary>
  /// The available options for this field
  /// </summary>
  [Required]
  public IList<SettingsFieldSelectOption> Options { get; set; } = new List<SettingsFieldSelectOption>(0);

  /// <summary>
  /// Creates a new <see cref="SettingsFieldSelect"/>
  /// </summary>
  /// <param name="name">The name (key) of field</param>
  /// <param name="label">The visible label of the field</param>
  /// <param name="description">An optional description of the field</param>
  /// <param name="required">Whether the field is required</param>
  public SettingsFieldSelect(string name, string label, string? description, bool required) :
    base(name, label, description, required)
  {
  }

  /// <summary>
  /// Adds a new option to the select field
  /// </summary>
  /// <param name="label">The visible label of the option</param>
  /// <param name="value">The value of the option</param>
  public void AddOption(string label, string value)
  {
    Options.Add(new SettingsFieldSelectOption(label, value));
  }
}

/// <summary>
/// An option for a <see cref="SettingsFieldSelect"/>
/// </summary>
public class SettingsFieldSelectOption
{
  /// <summary>
  /// The visible label of the option
  /// </summary>
  [Required]
  [MinLength(1)]
  [MaxLength(128)]
  public string Label { get; }

  /// <summary>
  /// The value of the option
  /// </summary>
  [Required]
  [MinLength(1)]
  [MaxLength(128)]
  [RegularExpression("^[\\w-]+$")]
  public string Value { get; }

  /// <summary>
  /// Creates a new <see cref="SettingsFieldSelectOption"/>
  /// </summary>
  /// <param name="label">The visible label</param>
  /// <param name="value">The option</param>
  public SettingsFieldSelectOption(string label, string value)
  {
    Label = label;
    Value = value;
  }
}