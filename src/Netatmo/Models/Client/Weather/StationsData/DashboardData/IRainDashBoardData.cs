using System.Text.Json.Serialization;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData;

public interface IRainDashBoardData : IDashBoardData
{
    [JsonPropertyName("Rain")]
    double Rain { get; set; }

    [JsonPropertyName("sum_rain_1")]
    double RainLastHour { get; set; }

    [JsonPropertyName("sum_rain_24")]
    double RainLastDay { get; set; }
}