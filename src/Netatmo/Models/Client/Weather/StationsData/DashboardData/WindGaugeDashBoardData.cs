using System.Text.Json.Serialization;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData;

public class WindGaugeDashBoardData : DashBoardData, IWindDashBoardData, IGustDashBoardData
{
    [JsonPropertyName("GustStrength")]
    public int GustStrength { get; set; }

    [JsonPropertyName("GustAngle")]
    public int GustAngle { get; set; }

    [JsonPropertyName("WindHistoric")]
    public WindHistoric[] WindHistoric { get; set; }

    [JsonPropertyName("WindStrength")]
    public int WindStrength { get; set; }

    [JsonPropertyName("WindAngle")]
    public int WindAngle { get; set; }
}