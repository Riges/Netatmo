using System.Text.Json.Serialization;

namespace Netatmo.Models.Client.Energy.HomeStatus;

public class Home
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("modules")]
    public Module[] Modules { get; set; }

    [JsonPropertyName("rooms")]
    public Room[] Rooms { get; set; }
}