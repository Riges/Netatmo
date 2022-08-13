using Newtonsoft.Json;

namespace Netatmo.Models.Client.Energy;

public class GetHomesDataRequest
{
    [JsonProperty("home_id")]
    public string HomeId { get; set; }

    [JsonProperty("gateway_types")]
    public string GatewayTypes { get; set; }
}