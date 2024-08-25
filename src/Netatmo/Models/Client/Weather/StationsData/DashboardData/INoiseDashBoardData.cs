using System.Text.Json.Serialization;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData;

public interface INoiseDashBoardData : IDashBoardData
{
    [JsonPropertyName("Noise")]
    double Noise { get; set; }
}