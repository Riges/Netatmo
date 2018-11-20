using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Weather.StationsData
{
    public interface IGustDashBoardData
    {
        [JsonProperty("time_utc")]
        Instant TimeUtc { get; set; }

        [JsonProperty("GustStrength")]
        int GustStrength { get; set; }

        [JsonProperty("GustAngle")]
        int GustAngle { get; set; }
    }
}