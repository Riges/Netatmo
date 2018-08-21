using Netatmo.Converters;
using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Weather
{
    public class DashBoardData
    {
        [JsonProperty("time_utc")]
        [JsonConverter(typeof(TimestampToLocalDateTimeConverter))]
        public LocalDateTime TimeUtc { get; set; }
    }
}