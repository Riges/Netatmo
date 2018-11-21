using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData
{
    public interface IDashBoardData
    {
        [JsonProperty("time_utc")]
        Instant TimeUtc { get; set; }
    }
}