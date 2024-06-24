using System.ComponentModel.DataAnnotations;

namespace MoBro.Plugin.SDK.Models.Settings;

/// <summary>
/// A textual settings field
/// </summary>
public sealed class SettingsFieldString : SettingsFieldBase
{
  /// <summary>
  /// The default value of the field
  /// </summary>
  [MaxLength(128)]
  public string? DefaultValue { get; set; }

  /// <summary>
  /// An optional regex to validate the field
  /// </summary>
  [MaxLength(128)]
  public string? Regex { get; set; }

  /// <summary>
  /// Creates a new <see cref="SettingsFieldString"/>
  /// </summary>
  /// <param name="name">The name (key) of field</param>
  /// <param name="label">The visible label of the field</param>
  /// <param name="description">An optional description of the field</param>
  /// <param name="required">Whether the field is required</param>
  public SettingsFieldString(string name, string label, string? description, bool required) :
    base(name, label, description, required)
  {
  }
}