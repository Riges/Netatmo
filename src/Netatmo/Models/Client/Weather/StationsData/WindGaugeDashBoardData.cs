using Newtonsoft.Json;

namespace Netatmo.Models.Client.Weather.StationsData
{
    public class WindGaugeDashBoardData : DashBoardData
    {
        [JsonProperty("WindStrength")]
        public int WindStrength { get; set; }

        [JsonProperty("WindAngle")]
        public int WindAngle { get; set; }

        [JsonProperty("GustStrength")]
        public double GustStrength { get; set; }

        [JsonProperty("GustAngle")]
        public int GustAngle { get; set; }

        [JsonProperty("WindHistoric")]
        public WindHistoric[] WindHistoric { get; set; }
    }
}