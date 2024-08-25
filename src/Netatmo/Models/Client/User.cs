using System.Text.Json.Serialization;
using Netatmo.Models.Client.Weather.StationsData;

namespace Netatmo.Models.Client;

public record User(
    [property: JsonPropertyName("administrative")] Administrative Administrative,
    [property: JsonPropertyName("mail")] string Mail);