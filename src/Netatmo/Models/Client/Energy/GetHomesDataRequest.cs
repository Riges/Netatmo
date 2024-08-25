using System.Text.Json.Serialization;

namespace Netatmo.Models.Client.Energy;

public class GetHomesDataRequest
{
    [JsonPropertyName("home_id")]
    public string HomeId { get; set; }

    [JsonPropertyName("gateway_types")]
    public string GatewayTypes { get; set; }
}