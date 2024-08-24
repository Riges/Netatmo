using Netatmo.Models.Client.Weather.StationsData;
using Newtonsoft.Json;

namespace Netatmo.Models.Client;

public record User(
    [property: JsonProperty("administrative")]
    Administrative Administrative,
    [property: JsonProperty("mail")] string Mail);