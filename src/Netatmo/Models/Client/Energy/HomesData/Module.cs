using System.Text.Json.Serialization;
using NodaTime;

namespace Netatmo.Models.Client.Energy.HomesData;

public class Module
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    // NATherm1 = thermostat, NRV = valve, NAPlug = relay, NACamera = welcome camera, NOC = presence camera
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("setup_date")]
    public Instant SetupAt { get; set; }

    [JsonPropertyName("modules_bridged")]
    public string[] ModulesBridged { get; set; }

    [JsonPropertyName("bridge")]
    public string Bridge { get; set; }

    [JsonPropertyName("room_id")]
    public string RoomId { get; set; }
}