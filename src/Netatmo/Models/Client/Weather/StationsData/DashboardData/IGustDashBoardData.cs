using Newtonsoft.Json;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData;

public interface IGustDashBoardData : IDashBoardData
{
    [JsonProperty("GustStrength")]
    int GustStrength { get; set; }

    [JsonProperty("GustAngle")]
    int GustAngle { get; set; }
}