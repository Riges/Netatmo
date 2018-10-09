using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Energy.HomesData
{
    public class Home
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("timezone")]
        public DateTimeZone Timezone { get; set; }

        [JsonProperty("schedules")]
        public Schedule[] Schedules { get; set; }

        [JsonProperty("coordinates")]
        public double[] Coordinates { get; set; }

        [JsonProperty("therm_setpoint_default_duration")]
        public int ThermSetpointDefaultDuration { get; set; }

        [JsonProperty("therm_mode")]
        public string ThermMode { get; set; }

        [JsonProperty("therm_mode_endtime")]
        public LocalDateTime? ThermModeEndtime { get; set; }

        [JsonProperty("rooms")]
        public Room[] Rooms { get; set; }

        [JsonProperty("modules")]
        public Module[] Modules { get; set; }
    }
}