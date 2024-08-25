using System.Text.Json.Serialization;

namespace Netatmo.Models.Client.Energy.HomesData;

public class Zone : Energy.Zone
{
    [JsonPropertyName("rooms_temp")]
    public RoomTemp[] RoomsTemp { get; set; }

    public class RoomTemp
    {
        [JsonPropertyName("room_id")]
        public string RoomId { get; set; }

        [JsonPropertyName("temp")]
        public double Temp { get; set; }
    }
}