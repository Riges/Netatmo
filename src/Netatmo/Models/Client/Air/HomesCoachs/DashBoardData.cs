using System.Text.Json.Serialization;
using NodaTime;

namespace Netatmo.Models.Client.Air.HomesCoachs;

public class DashBoardData
{
    [JsonPropertyName("time_utc")]
    public Instant TimeUtc { get; set; }

    [JsonPropertyName("Temperature")]
    public double Temperature { get; set; }

    [JsonPropertyName("CO2")]
    public int CO2 { get; set; }

    [JsonPropertyName("Humidity")]
    public int HumidityPercent { get; set; }

    [JsonPropertyName("Noise")]
    public double Noise { get; set; }

    [JsonPropertyName("Pressure")]
    public double Pressure { get; set; }

    [JsonPropertyName("AbsolutePressure")]
    public double AbsolutePressure { get; set; }

    [JsonPropertyName("health_idx")]
    public HealthIdx HealthIdx { get; set; }

    [JsonPropertyName("min_temp")]
    public decimal MinTemp { get; set; }

    [JsonPropertyName("max_temp")]
    public decimal MaxTemp { get; set; }

    [JsonPropertyName("date_min_temp")]
    public Instant DateMinTemp { get; set; }

    [JsonPropertyName("date_max_temp")]
    public Instant DateMaxTemp { get; set; }
}