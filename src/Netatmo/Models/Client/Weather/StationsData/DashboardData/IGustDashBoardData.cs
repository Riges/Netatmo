using System.Text.Json.Serialization;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData;

public interface IGustDashBoardData : IDashBoardData
{
    [JsonPropertyName("GustStrength")]
    int GustStrength { get; set; }

    [JsonPropertyName("GustAngle")]
    int GustAngle { get; set; }
}