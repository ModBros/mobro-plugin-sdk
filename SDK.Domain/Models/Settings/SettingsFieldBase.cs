using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MoBro.Plugin.SDK.Models.Settings;

/// <summary>
/// Abstract base class for all setting fields
/// </summary>
[JsonPolymorphic(
  TypeDiscriminatorPropertyName = "type",
  UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization,
  IgnoreUnrecognizedTypeDiscriminators = false
)]
[JsonDerivedType(typeof(SettingsFieldCheckbox), typeDiscriminator: "checkbox")]
[JsonDerivedType(typeof(SettingsFieldNumber), typeDiscriminator: "number")]
[JsonDerivedType(typeof(SettingsFieldSelect), typeDiscriminator: "select")]
[JsonDerivedType(typeof(SettingsFieldString), typeDiscriminator: "string")]
public abstract class SettingsFieldBase
{
  /// <summary>
  /// The name (key) of the field
  /// </summary>
  [Required]
  [MinLength(1)]
  [MaxLength(128)]
  [RegularExpression("^[\\w-]+$")]
  public string Name { get; }

  /// <summary>
  /// The visible label of the field
  /// </summary>
  [Required]
  [MinLength(1)]
  [MaxLength(128)]
  public string Label { get; }

  /// <summary>
  /// An optional description of the field
  /// </summary>
  public string? Description { get; }

  /// <summary>
  /// Whether the field is required
  /// </summary>
  public bool Required { get; }

  private protected SettingsFieldBase(string name, string label, string? description, bool required)
  {
    Name = name;
    Label = label;
    Description = description;
    Required = required;
  }
}