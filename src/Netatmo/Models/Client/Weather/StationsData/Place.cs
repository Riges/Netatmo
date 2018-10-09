using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Weather.StationsData
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
        public DateTimeZone Timezone { get; set; }

        [JsonProperty("location")]
        public double[] Location { get; set; }
    }
}