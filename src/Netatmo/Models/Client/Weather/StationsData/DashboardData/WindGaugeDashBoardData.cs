using Newtonsoft.Json;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData;

public class WindGaugeDashBoardData : DashBoardData, IWindDashBoardData, IGustDashBoardData
{
    [JsonProperty("GustStrength")]
    public int GustStrength { get; set; }

    [JsonProperty("GustAngle")]
    public int GustAngle { get; set; }

    [JsonProperty("WindHistoric")]
    public WindHistoric[] WindHistoric { get; set; }

    [JsonProperty("WindStrength")]
    public int WindStrength { get; set; }

    [JsonProperty("WindAngle")]
    public int WindAngle { get; set; }
}