using System.Text.Json.Serialization;
using NodaTime;

namespace Netatmo.Models.Client.Energy.HomesData;

public class Home
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("timezone")]
    public DateTimeZone Timezone { get; set; }

    [JsonPropertyName("schedules")]
    public Schedule[] Schedules { get; set; }

    [JsonPropertyName("coordinates")]
    public double[] Coordinates { get; set; }

    [JsonPropertyName("therm_setpoint_default_duration")]
    public int ThermSetpointDefaultDuration { get; set; }

    [JsonPropertyName("therm_mode")]
    public string ThermMode { get; set; }

    [JsonPropertyName("therm_mode_endtime")]
    public Instant? ThermModeEndtime { get; set; }

    [JsonPropertyName("rooms")]
    public Room[] Rooms { get; set; }

    [JsonPropertyName("modules")]
    public Module[] Modules { get; set; }
}