using System.Text.Json.Serialization;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData;

public interface ICO2DashBoardData : IDashBoardData
{
    [JsonPropertyName("CO2")]
    int CO2 { get; set; }
}