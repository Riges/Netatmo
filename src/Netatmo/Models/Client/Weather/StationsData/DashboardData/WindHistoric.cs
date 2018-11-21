using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData
{
    public class WindHistoric : IWindHistory
    {
        [JsonProperty("WindStrength")]
        public int WindStrength { get; set; }

        [JsonProperty("WindAngle")]
        public int WindAngle { get; set; }

        [JsonProperty("time_utc")]
        public Instant TimeUtc { get; set; }
    }
}