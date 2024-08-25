using System.Text.Json.Serialization;
using Netatmo.Models.Client.Energy.HomeStatus;

namespace Netatmo.Models.Client.Energy;

public class GetHomeStatusBody
{
    [JsonPropertyName("home")]
    public Home Home { get; set; }
}