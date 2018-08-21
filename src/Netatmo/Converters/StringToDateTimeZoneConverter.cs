using System;
using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Converters
{
    public class StringToDateTimeZoneConverter : JsonConverter{
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return null;
            }

            return DateTimeZoneProviders.Tzdb[reader.Value.ToString()];
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}