using Netatmo.Converters;
using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Weather
{
    public class WindHistoric
    {
        [JsonProperty("WindStrength")]
        public int WindStrength { get; set; }

        [JsonProperty("WindAngle")]
        public int WindAngle { get; set; }

        [JsonProperty("time_utc")]
        [JsonConverter(typeof(TimestampToLocalDateTimeConverter))]
        public LocalDateTime TimeUtc { get; set; }
    }
}