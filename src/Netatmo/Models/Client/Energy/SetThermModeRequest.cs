using System.Text.Json.Serialization;
using NodaTime;

namespace Netatmo.Models.Client.Energy;

public class SetThermModeRequest
{
    [JsonPropertyName("home_id")]
    public string HomeId { get; set; }

    [JsonPropertyName("mode")]
    public string Mode { get; set; }

    [JsonPropertyName("endtime")]
    public Instant? EndTime { get; set; }
}