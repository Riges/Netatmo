using System.Text.Json.Serialization;
using NodaTime;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData;

public interface IDashBoardData
{
    [JsonPropertyName("time_utc")]
    Instant TimeUtc { get; set; }
}