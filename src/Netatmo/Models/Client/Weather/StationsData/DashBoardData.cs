using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Weather.StationsData
{
    public class DashBoardData
    {
        [JsonProperty("time_utc")]
        public Instant TimeUtc { get; set; }
    }
}