using Netatmo.Models.Client.Weather.StationsData;
using Newtonsoft.Json;

namespace Netatmo.Models.Client.Air.HomesCoachs
{
    public class User
    {
        [JsonProperty("mail")]
        public string Mail { get; set; }
        
        [JsonProperty("administrative")]
        public Administrative Administrative { get; set; }
    }
}