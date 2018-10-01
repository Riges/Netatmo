using Newtonsoft.Json;

namespace Netatmo.Models.Client.Energy
{
    public class GetHomesDataRequest
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("home_id")]
        public string HomeId { get; set; }

        [JsonProperty("gateway_types")]
        public string GatewayTypes { get; set; }
    }
}