using Newtonsoft.Json;

namespace Netatmo.Models.Client.Energy.HomesData;

public class Zone : Energy.Zone
{
    [JsonProperty("rooms_temp")]
    public RoomTemp[] RoomsTemp { get; set; }

    public class RoomTemp
    {
        [JsonProperty("room_id")]
        public string RoomId { get; set; }

        [JsonProperty("temp")]
        public double Temp { get; set; }
    }
}