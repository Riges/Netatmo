using Newtonsoft.Json;

namespace Netatmo.Models.Client.Weather
{
    public class IndoorDashBoardData : OutdoorDashBoardData
    {
        [JsonProperty("CO2")]
        public int CO2 { get; set; }
    }
}