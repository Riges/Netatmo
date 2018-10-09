using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Energy
{
    public class SetThermModeRequest
    {
        [JsonProperty("home_id")]
        public string HomeId { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("endtime")]
        public LocalDateTime? EndTime { get; set; }
    }
}