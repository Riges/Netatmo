using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Air.HomesCoachs;

public class DashBoardData
{
    [JsonProperty("time_utc")] 
    public Instant TimeUtc { get; set; }

    [JsonProperty("Temperature")] 
    public double Temperature { get; set; }

    [JsonProperty("CO2")] 
    public int CO2 { get; set; }

    [JsonProperty("Humidity")] 
    public int HumidityPercent { get; set; }

    [JsonProperty("Noise")] 
    public double Noise { get; set; }

    [JsonProperty("Pressure")] 
    public double Pressure { get; set; }

    [JsonProperty("AbsolutePressure")]
    public double AbsolutePressure { get; set; }

    [JsonProperty("health_idx")] 
    public HealthIdx HealthIdx { get; set; }

    [JsonProperty("min_temp")] 
    public decimal MinTemp { get; set; }

    [JsonProperty("max_temp")] 
    public decimal MaxTemp { get; set; }

    [JsonProperty("date_min_temp")]
    public Instant DateMinTemp { get; set; }

    [JsonProperty("date_max_temp")] 
    public Instant DateMaxTemp { get; set; }
}