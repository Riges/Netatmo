using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Weather.StationsData
{
    public interface IHumidityDashBoardData
    {
        [JsonProperty("time_utc")]
        Instant TimeUtc { get; set; }

        [JsonProperty("Humidity")]
        int HumidityPercent { get; set; }
    }
}