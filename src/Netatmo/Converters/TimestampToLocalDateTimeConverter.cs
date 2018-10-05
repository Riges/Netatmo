using System;
using Flurl.Util;
using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Converters
{
    public class TimestampToLocalDateTimeConverter : JsonConverter
    {
        private static readonly LocalDateTime _epoch = new LocalDateTime(1970, 1, 1, 0, 0, 0);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value is LocalDateTime localDateTime && localDateTime != default(LocalDateTime)
                ? localDateTime.ToInvariantString()
                : null);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) return null;

            var value = int.Parse(reader.Value.ToString());

            if (value == 0) return null;

            return _epoch.PlusSeconds(value);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(int);
        }
    }
}