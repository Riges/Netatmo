using System.Text.Json.Serialization;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData;

public class RainGaugeDashBoardData : DashBoardData, IRainDashBoardData
{
    [JsonPropertyName("Rain")]
    public double Rain { get; set; }

    [JsonPropertyName("sum_rain_1")]
    public double RainLastHour { get; set; }

    [JsonPropertyName("sum_rain_24")]
    public double RainLastDay { get; set; }
}