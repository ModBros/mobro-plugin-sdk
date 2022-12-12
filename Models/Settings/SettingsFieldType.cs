namespace MoBro.Plugin.SDK.Models.Settings;

/// <summary>
/// The type of a <see cref="SettingsFieldBase"/>
/// </summary>
public enum SettingsFieldType
{
  /// <summary>
  /// A checkbox field
  /// </summary>
  Checkbox,

  /// <summary>
  /// A numerical field
  /// </summary>
  Number,

  /// <summary>
  /// A textual field
  /// </summary>
  String,

  /// <summary>
  /// A select field
  /// </summary>
  Select
}