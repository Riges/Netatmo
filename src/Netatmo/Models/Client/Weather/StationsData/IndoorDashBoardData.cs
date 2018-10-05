using Newtonsoft.Json;

namespace Netatmo.Models.Client.Weather.StationsData
{
    public class IndoorDashBoardData : OutdoorDashBoardData
    {
        [JsonProperty("CO2")]
        public int CO2 { get; set; }
    }
}