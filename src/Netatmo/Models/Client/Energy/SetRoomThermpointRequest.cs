using System.Text.Json.Serialization;
using NodaTime;

namespace Netatmo.Models.Client.Energy;

public class SetRoomThermpointRequest
{
    [JsonPropertyName("home_id")]
    public string HomeId { get; set; }

    [JsonPropertyName("room_id")]
    public string RoomId { get; set; }

    [JsonPropertyName("mode")]
    public string Mode { get; set; }

    [JsonPropertyName("temp")]
    public double? Temp { get; set; }

    [JsonPropertyName("endtime")]
    public Instant? EndTime { get; set; }
}