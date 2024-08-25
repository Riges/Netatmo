using System.Text.Json.Serialization;

namespace Netatmo.Models.Client.Energy;

public class SwitchHomeScheduleRequest
{
    [JsonPropertyName("home_id")]
    public string HomeId { get; set; }

    [JsonPropertyName("schedule_id")]
    public string ScheduleId { get; set; }
}