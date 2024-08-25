using System.Text.Json.Serialization;

namespace Netatmo.Models.Client.Weather;

public class GetStationsDataRequest
{
    [JsonPropertyName("device_id")]
    public string DeviceId { get; set; }

    [JsonPropertyName("get_favorites")]
    public bool? GetFavorites { get; set; }
}