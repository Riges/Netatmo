using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Weather.StationsData
{
    public interface IRainDashBoardData
    {
        [JsonProperty("time_utc")]
        Instant TimeUtc { get; set; }

        [JsonProperty("Rain")]
        double Rain { get; set; }

        [JsonProperty("sum_rain_1")]
        double RainLastHour { get; set; }

        [JsonProperty("sum_rain_24")]
        double RainLastDay { get; set; }
    }
}