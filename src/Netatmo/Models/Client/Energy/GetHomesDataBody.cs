using Netatmo.Models.Client.Energy.HomesData;
using Newtonsoft.Json;

namespace Netatmo.Models.Client.Energy;

public class GetHomesDataBody
{
    [JsonProperty("homes")]
    public Home[] Homes { get; set; }

    [JsonProperty("user")]
    public User User { get; set; }
}