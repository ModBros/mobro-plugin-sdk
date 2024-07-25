using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using MoBro.Plugin.SDK.Models.Metrics;

namespace MoBro.Plugin.SDK.Json;

/// <summary>
/// Converts JSON data to a <see cref="MetricValue"/> object and vice versa.
/// </summary>
public class MetricValueConverter : JsonConverter<MetricValue>
{
  /// <inheritdoc />
  public override MetricValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    if (reader.TokenType != JsonTokenType.StartObject)
    {
      throw new JsonException();
    }

    string? id = null;
    DateTimeOffset? timestamp = null;
    object? value = null;

    while (reader.Read())
    {
      switch (reader.TokenType)
      {
        case JsonTokenType.EndObject:
          if (id == null || timestamp == null)
          {
            throw new JsonException();
          }

          return new MetricValue(id, timestamp.Value, value);

        case JsonTokenType.PropertyName:
        {
          var propertyName = reader.GetString();
          reader.Read();

          switch (propertyName)
          {
            case "Id":
              id = reader.GetString();
              break;
            case "Timestamp":
              timestamp = reader.GetDateTimeOffset();
              break;
            case "Value":
              value = reader.TokenType switch
              {
                JsonTokenType.False => false,
                JsonTokenType.True => true,
                JsonTokenType.String => reader.GetString(),
                JsonTokenType.Number => reader.GetDouble(),
                _ => null
              };
              break;
          }

          break;
        }
        default:
          continue;
      }
    }

    throw new JsonException();
  }

  /// <inheritdoc />
  public override void Write(Utf8JsonWriter writer, MetricValue value, JsonSerializerOptions options)
  {
    writer.WriteStartObject();

    writer.WriteString(nameof(value.Id), value.Id);

    writer.WritePropertyName(nameof(value.Timestamp));
    JsonSerializer.Serialize(writer, value.Timestamp, options);

    writer.WritePropertyName(nameof(value.Value));
    JsonSerializer.Serialize(writer, value.Value, options);

    writer.WriteEndObject();
  }
}