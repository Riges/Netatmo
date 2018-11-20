using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Weather.StationsData
{
    public interface ICO2DashBoardData
    {
        [JsonProperty("time_utc")]
        Instant TimeUtc { get; set; }

        [JsonProperty("CO2")]
        int CO2 { get; set; }
    }
}