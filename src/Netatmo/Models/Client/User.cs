using Netatmo.Models.Client.Weather.StationsData;
using Newtonsoft.Json;

namespace Netatmo.Models.Client
{
    public class User
    {
        [JsonProperty("administrative")]
        public Administrative Administrative { get; set; }

        [JsonProperty("mail")]
        public string Mail { get; set; }
    }
}