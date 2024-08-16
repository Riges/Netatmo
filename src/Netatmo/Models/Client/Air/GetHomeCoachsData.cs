using Netatmo.Models.Client.Air.HomesCoachs;
using Newtonsoft.Json;

namespace Netatmo.Models.Client.Air;

public class GetHomeCoachsData
{
    [JsonProperty("devices")]
    public Devices[] Devices { get; set; }

    [JsonProperty("user")]
    public User User { get; set; }
}