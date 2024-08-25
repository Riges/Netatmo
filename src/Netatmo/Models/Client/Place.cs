using System.Text.Json.Serialization;
using NodaTime;

namespace Netatmo.Models.Client;

public record Place(
    [property: JsonPropertyName("altitude")] double Altitude,
    [property: JsonPropertyName("city")] string City,
    [property: JsonPropertyName("country")] string Country,
    [property: JsonPropertyName("timezone")] DateTimeZone Timezone,
    [property: JsonPropertyName("location")] double[] Location);