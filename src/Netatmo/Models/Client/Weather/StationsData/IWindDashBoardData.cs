using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Weather.StationsData
{
    public interface IWindDashBoardData
    {
        [JsonProperty("time_utc")]
        Instant TimeUtc { get; set; }

        [JsonProperty("WindStrength")]
        int WindStrength { get; set; }

        [JsonProperty("WindAngle")]
        int WindAngle { get; set; }
    }
}