using Netatmo.Models.Client.Energy.HomeStatus;
using Newtonsoft.Json;

namespace Netatmo.Models.Client.Energy;

public class GetHomeStatusBody
{
    [JsonProperty("home")]
    public Home Home { get; set; }
}