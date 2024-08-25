using System.Text.Json.Serialization;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData;

public interface IHumidityDashBoardData : IDashBoardData
{
    [JsonPropertyName("Humidity")]
    int HumidityPercent { get; set; }
}