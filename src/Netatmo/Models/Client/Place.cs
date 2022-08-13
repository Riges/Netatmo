using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client;

public record Place(
    [property: JsonProperty("altitude")] double Altitude,
    [property: JsonProperty("city")] string City,
    [property: JsonProperty("country")] string Country,
    [property: JsonProperty("timezone")] DateTimeZone Timezone,
    [property: JsonProperty("location")] double[] Location);