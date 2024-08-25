using System.Text.Json.Serialization;

namespace Netatmo.Models.Client.Energy;

public class GetHomeStatusRequest
{
    [JsonPropertyName("home_id")]
    public string HomeId { get; set; }

    [JsonPropertyName("device_types")]
    public string[] DeviceTypes { get; set; }
}