using System.Text.Json;
using System.Text.Json.Serialization;

namespace Netatmo.Converters;

public class ZoneIdConverter : JsonConverter<string>
{
    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.TokenType switch
        {
            JsonTokenType.String => reader.GetString(),
            JsonTokenType.Number => reader.GetInt32().ToString(),
            _ => throw new JsonException("Zone.Id must be a string or an integer.")
        };

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options) => writer.WriteNumberValue(int.Parse(value));
}