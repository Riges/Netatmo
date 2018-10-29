using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Energy
{
    public class GetRoomMeasureRequest
    {
        [JsonProperty("home_id")]
        public string HomeId { get; set; }

        [JsonProperty("room_id")]
        public string RoomId { get; set; }

        [JsonProperty("scale")]
        public string Scale { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("date_begin")]
        public Instant? BeginAt { get; set; }

        [JsonProperty("date_end")]
        public Instant? EndAt { get; set; }

        [JsonProperty("limit")]
        public int? Limit { get; set; }

        [JsonProperty("optimize")]
        public bool? Optimize { get; set; }

        [JsonProperty("real_time")]
        public bool? RealTime { get; set; }
    }
}