using Netatmo.Converters;
using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Energy
{
    public class Module
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        // NATherm1 = thermostat, NRV = valve, NAPlug = relay, NACamera = welcome camera, NOC = presence camera
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("setup_date")]
        [JsonConverter(typeof(TimestampToLocalDateTimeConverter))]
        public LocalDateTime SetupAt { get; set; }
        
        [JsonProperty("modules_bridged")]
        public string[] ModulesBridged { get; set; }
        
        [JsonProperty("bridge")]
        public string Bridge { get; set; }

        [JsonProperty("room_id")]
        public string RoomId { get; set; }
    }
}