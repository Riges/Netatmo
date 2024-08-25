using System.Text.Json;
using System.Text.Json.Serialization;
using NodaTime;

namespace Netatmo.Converters;

public class StringToDateTimeZoneConverter : JsonConverter<DateTimeZone>
{
    public override DateTimeZone Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        DateTimeZoneProviders.Tzdb.GetZoneOrNull(reader.GetString()!);

    public override void Write(Utf8JsonWriter writer, DateTimeZone value, JsonSerializerOptions options) => writer.WriteStringValue(value?.Id);
}