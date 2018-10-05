using Newtonsoft.Json;

namespace Netatmo.Models.Client.Weather
{
    public class GetStationsDataRequest
    {
        [JsonProperty("device_id")]
        public string DeviceId { get; set; }

        [JsonProperty("get_favorites")]
        public bool? GetFavorites { get; set; }
    }
}