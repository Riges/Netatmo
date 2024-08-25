using System.Text.Json.Serialization;
using NodaTime;

namespace Netatmo.Models.Client.Energy.HomeStatus;

public class Room
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("reachable")]
    public bool Reachable { get; set; }

    [JsonPropertyName("anticipating")]
    public bool Anticipating { get; set; }

    [JsonPropertyName("open_window")]
    public bool OpenWindow { get; set; }

    [JsonPropertyName("therm_measured_temperature")]
    public double ThermMeasuredTemperature { get; set; }

    [JsonPropertyName("therm_setpoint_temperature")]
    public double ThermSetPointTemperature { get; set; }

    [JsonPropertyName("heating_power_request")]
    public int? HeatingPowerRequest { get; set; }

    [JsonPropertyName("therm_setpoint_mode")]
    public string ThermSetPointMode { get; set; }

    [JsonPropertyName("therm_setpoint_start_time")]
    public Instant ThermSetPointStartTime { get; set; }

    [JsonPropertyName("therm_setpoint_end_time")]
    public Instant? ThermSetPointEndTime { get; set; }
}