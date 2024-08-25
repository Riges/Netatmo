using System.Text.Json.Serialization;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData;

public interface IPressureDashBoardData : IDashBoardData
{
    [JsonPropertyName("AbsolutePressure")]
    double AbsolutePressure { get; set; }

    [JsonPropertyName("Pressure")]
    double Pressure { get; set; }

    // pressure_trend for last 12h (up, down, stable)
    [JsonPropertyName("pressure_trend")]
    string PressureTrend { get; set; }
}