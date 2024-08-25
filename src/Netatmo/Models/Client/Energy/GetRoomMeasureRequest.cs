using System.Text.Json.Serialization;
using NodaTime;

namespace Netatmo.Models.Client.Energy;

public class GetRoomMeasureRequest
{
    [JsonPropertyName("home_id")]
    public string HomeId { get; set; }

    [JsonPropertyName("room_id")]
    public string RoomId { get; set; }

    [JsonPropertyName("scale")]
    public string Scale { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("date_begin")]
    public Instant? BeginAt { get; set; }

    [JsonPropertyName("date_end")]
    public Instant? EndAt { get; set; }

    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    [JsonPropertyName("optimize")]
    public bool? Optimize { get; set; }

    [JsonPropertyName("real_time")]
    public bool? RealTime { get; set; }
}