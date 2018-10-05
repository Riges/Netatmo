using Netatmo.Models.Client.Weather.StationsData;
using Newtonsoft.Json;

namespace Netatmo.Models.Client.Weather
{
    public class GetStationsDataBody
    {
        [JsonProperty("devices")]
        public Device[] Devices { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}