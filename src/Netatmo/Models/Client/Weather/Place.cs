using Netatmo.Converters;
using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Weather
{
    public class Place
    {
        [JsonProperty("altitude")]
        public double Altitude { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("timezone")]
        [JsonConverter(typeof(StringToDateTimeZoneConverter))]
        public DateTimeZone Timezone { get; set; }
        
        [JsonProperty("location")]
        public double[] Location { get; set; }
    }
}