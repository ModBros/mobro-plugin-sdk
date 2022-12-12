using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MoBro.Plugin.SDK.Models.Settings;

internal class SettingsFieldConverter : JsonConverter<SettingsFieldBase>
{
  // TODO replace this class with .NET 7 polymorphic deserialization
  // https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/polymorphism?pivots=dotnet-7-0


  public override SettingsFieldBase? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    // not required in SDK
    throw new NotImplementedException();
  }

  public override void Write(Utf8JsonWriter writer, SettingsFieldBase value, JsonSerializerOptions options)
  {
    switch (value)
    {
      case SettingsFieldCheckbox boolValue:
        JsonSerializer.Serialize(writer, boolValue, options);
        break;
      case SettingsFieldNumeric numericValue:
        JsonSerializer.Serialize(writer, numericValue, options);
        break;
      case SettingsFieldString stringValue:
        JsonSerializer.Serialize(writer, stringValue, options);
        break;
      case SettingsFieldSelect selectValue:
        JsonSerializer.Serialize(writer, selectValue, options);
        break;
      default:
        throw new JsonException("Unknown SettingsField type");
    }
  }
}