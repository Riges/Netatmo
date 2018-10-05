using Newtonsoft.Json;

namespace Netatmo.Models.Client.Energy
{
    public class GetHomeStatusRequest
    {
        [JsonProperty("home_id")]
        public string HomeId { get; set; }

        [JsonProperty("device_types")]
        public string[] DeviceTypes { get; set; }
    }
}