using Newtonsoft.Json;

namespace Netatmo.Models.Client.Energy.HomeStatus
{
    public class Home
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("modules")]
        public Module[] Modules { get; set; }

        [JsonProperty("rooms")]
        public Room[] Rooms { get; set; }
    }
}