namespace MoBro.Plugin.SDK.Models.Settings;

/// <summary>
/// A numeric settings field
/// </summary>
public class SettingsFieldNumeric : SettingsFieldBase
{
  /// <summary>
  /// The default value of the field
  /// </summary>
  public double? Default { get; set; }

  /// <summary>
  /// The minimum value of the field
  /// </summary>
  public double? Min { get; set; }

  /// <summary>
  /// The maximum value of the field
  /// </summary>
  public double? Max { get; set; }

  /// <summary>
  /// Creates a new <see cref="SettingsFieldNumeric"/>
  /// </summary>
  /// <param name="name">The name (key) of field</param>
  /// <param name="label">The visible label of the field</param>
  /// <param name="description">An optional description of the field</param>
  /// <param name="required">Whether the field is required</param>
  public SettingsFieldNumeric(string name, string label, string? description, bool required) :
    base(SettingsFieldType.Number, name, label, description, required)
  {
  }
}