using System.Text.Json.Serialization;
using NodaTime;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData;

public class WindHistoric : IWindHistory
{
    [JsonPropertyName("WindStrength")]
    public int WindStrength { get; set; }

    [JsonPropertyName("WindAngle")]
    public int WindAngle { get; set; }

    [JsonPropertyName("time_utc")]
    public Instant TimeUtc { get; set; }
}