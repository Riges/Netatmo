using System.Text.Json.Serialization;
using Netatmo.Models.Client.Air.HomesCoachs;

namespace Netatmo.Models.Client.Air;

public class GetHomeCoachsData
{
    [JsonPropertyName("devices")]
    public Devices[] Devices { get; set; }

    [JsonPropertyName("user")]
    public User User { get; set; }
}