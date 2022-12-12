namespace MoBro.Plugin.SDK.Models.Settings;

/// <summary>
/// A checkbox settings field
/// </summary>
public class SettingsFieldCheckbox : SettingsFieldBase
{
  /// <summary>
  /// The default value of the field
  /// </summary>
  public bool? Default { get; set; }

  /// <summary>
  /// Creates a new <see cref="SettingsFieldCheckbox"/>
  /// </summary>
  /// <param name="name">The name (key) of field</param>
  /// <param name="label">The visible label of the field</param>
  /// <param name="description">An optional description of the field</param>
  /// <param name="required">Whether the field is required</param>
  public SettingsFieldCheckbox(string name, string label, string? description, bool required) :
    base(SettingsFieldType.Checkbox, name, label, description, required)
  {
  }
}