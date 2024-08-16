using Flurl.Util;
using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Converters;

public class StringToDateTimeZoneConverter : JsonConverter<DateTimeZone>
{
    public override void WriteJson(JsonWriter writer, DateTimeZone value, JsonSerializer serializer)
    {
        writer.WriteValue(value?.ToInvariantString());
    }

    public override DateTimeZone ReadJson(JsonReader reader, Type objectType, DateTimeZone existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.Value == null)
        {
            return null;
        }

        return DateTimeZoneProviders.Tzdb[reader.Value.ToString()];
    }
}