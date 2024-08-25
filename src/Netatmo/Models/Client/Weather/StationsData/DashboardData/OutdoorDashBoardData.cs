using System.Text.Json.Serialization;
using NodaTime;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData;

public class OutdoorDashBoardData : DashBoardData, ITemperatureDashBoardData, IHumidityDashBoardData
{
    [JsonPropertyName("Humidity")]
    public int HumidityPercent { get; set; }

    [JsonPropertyName("Temperature")]
    public double Temperature { get; set; }

    // temp_trend for last 12h (up, down, stable)
    [JsonPropertyName("temp_trend")]
    public string TempTrend { get; set; }

    [JsonPropertyName("min_temp")]
    public double MinTemp { get; set; }

    [JsonPropertyName("max_temp")]
    public double MaxTemp { get; set; }

    [JsonPropertyName("date_min_temp")]
    public Instant MinTempAt { get; set; }

    [JsonPropertyName("date_max_temp")]
    public Instant MaxTempAt { get; set; }
}