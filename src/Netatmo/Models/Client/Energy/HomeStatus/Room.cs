using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Energy.HomeStatus
{
    public class Room
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("reachable")]
        public bool Reachable { get; set; }

        [JsonProperty("anticipating")]
        public bool Anticipating { get; set; }

        [JsonProperty("open_window")]
        public bool OpenWindow { get; set; }

        [JsonProperty("therm_measured_temperature")]
        public double ThermMeasuredTemperature { get; set; }

        [JsonProperty("therm_setpoint_temperature")]
        public double ThermSetPointTemperature { get; set; }

        [JsonProperty("heating_power_request")]
        public int? HeatingPowerRequest { get; set; }

        [JsonProperty("therm_setpoint_mode")]
        public string ThermSetPointMode { get; set; }

        [JsonProperty("therm_setpoint_start_time")]
        public Instant ThermSetPointStartTime { get; set; }

        [JsonProperty("therm_setpoint_end_time")]
        public Instant? ThermSetPointEndTime { get; set; }
    }
}