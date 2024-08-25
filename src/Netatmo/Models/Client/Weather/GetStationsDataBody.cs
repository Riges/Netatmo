using System.Text.Json.Serialization;
using Netatmo.Models.Client.Weather.StationsData;

namespace Netatmo.Models.Client.Weather;

public class GetStationsDataBody
{
    [JsonPropertyName("devices")]
    public Device[] Devices { get; set; }

    [JsonPropertyName("user")]
    public User User { get; set; }
}