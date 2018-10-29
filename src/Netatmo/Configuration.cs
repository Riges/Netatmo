using System.Collections.Generic;
using Flurl.Http.Configuration;
using Netatmo.Converters;
using Newtonsoft.Json;

namespace Netatmo
{
    public static class Configuration
    {
        public static JsonSerializerSettings JsonSerializer()
        {
            return new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new List<JsonConverter> {new TimestampToInstantConverter(), new StringToDateTimeZoneConverter()}
            };
        }

        public static void ConfigureRequest(FlurlHttpSettings settings)
        {
            var jsonSettings = JsonSerializer();

            //jsonSettings.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
            settings.JsonSerializer = new NewtonsoftJsonSerializer(jsonSettings);
        }
    }
}