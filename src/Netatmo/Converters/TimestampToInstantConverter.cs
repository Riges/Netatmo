using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Converters;

public class TimestampToInstantConverter : JsonConverter<Instant?>
{
    public override void WriteJson(JsonWriter writer, Instant? value, JsonSerializer serializer)
    {
        if (value.HasValue) writer.WriteValue(value.Value.ToUnixTimeSeconds().ToString());
    }

    public override Instant? ReadJson(JsonReader reader, Type objectType, Instant? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        if (reader.Value == null) return null;
        var value = long.Parse(reader.Value.ToString());

        return Instant.FromUnixTimeSeconds(value);
    }
}