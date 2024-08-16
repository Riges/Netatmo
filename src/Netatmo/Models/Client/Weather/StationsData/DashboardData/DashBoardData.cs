using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData;

public class DashBoardData : IDashBoardData
{
    [JsonProperty("time_utc")]
    public Instant TimeUtc { get; set; }
}