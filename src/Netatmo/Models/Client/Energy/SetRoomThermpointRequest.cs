using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Energy
{
    public class SetRoomThermpointRequest
    {
        [JsonProperty("home_id")]
        public string HomeId { get; set; }

        [JsonProperty("room_id")]
        public string RoomId { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("temp")]
        public double? Temp { get; set; }

        [JsonProperty("endtime")]
        public Instant? EndTime { get; set; }
    }
}