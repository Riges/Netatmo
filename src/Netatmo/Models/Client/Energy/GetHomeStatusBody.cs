using Newtonsoft.Json;

namespace Netatmo.Models.Client.Energy
{
    public class GetHomeStatusBody
    {
        [JsonProperty("home")]
        public HomeStatus.Home Home { get; set; }
    }
}