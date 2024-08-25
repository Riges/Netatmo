using System.Text.Json.Serialization;
using NodaTime;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData;

public interface ITemperatureDashBoardData : IDashBoardData
{
    [JsonPropertyName("Temperature")]
    double Temperature { get; set; }

    // temp_trend for last 12h (up, down, stable)
    [JsonPropertyName("temp_trend")]
    string TempTrend { get; set; }

    [JsonPropertyName("min_temp")]
    double MinTemp { get; set; }

    [JsonPropertyName("max_temp")]
    double MaxTemp { get; set; }

    [JsonPropertyName("date_min_temp")]
    Instant MinTempAt { get; set; }

    [JsonPropertyName("date_max_temp")]
    Instant MaxTempAt { get; set; }
}