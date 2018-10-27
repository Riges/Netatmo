using System;
using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Converters
{
    public class TimestampToLocalDateTimeConverter : JsonConverter<LocalDateTime?>
    {
        private static readonly LocalDateTime Epoch = new LocalDateTime(1970, 1, 1, 0, 0, 0);

        public override void WriteJson(JsonWriter writer, LocalDateTime? value, JsonSerializer serializer)
        {
            if (value.HasValue)
            {
                var timestamp = (int) value.Value.ToDateTimeUnspecified().Subtract(Epoch.ToDateTimeUnspecified()).TotalSeconds;
                if (timestamp > 0) writer.WriteValue(timestamp.ToString());
            }
        }

        public override LocalDateTime? ReadJson(JsonReader reader, Type objectType, LocalDateTime? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) return null;

            var value = int.Parse(reader.Value.ToString());

            if (value == 0) return null;

            return Epoch.PlusSeconds(value);
        }
    }
}