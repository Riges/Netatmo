using Netatmo.Models.Client.Energy.Enums;
using Newtonsoft.Json;

namespace Netatmo.Models.Client.Energy.HomesData
{
    public class Zone
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public ZoneTypeEnum Type { get; set; }

        [JsonProperty("rooms")]
        public Room[] Rooms { get; set; }

        [JsonProperty("rooms_temp")]
        public RoomTemp[] RoomsTemp { get; set; }

        public class Room
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("therm_setpoint_temperature")]
            public double ThermSetPointTemperature { get; set; }
        }

        public class RoomTemp
        {
            [JsonProperty("room_id")]
            public string RoomId { get; set; }

            [JsonProperty("temp")]
            public double Temp { get; set; }
        }
    }
}