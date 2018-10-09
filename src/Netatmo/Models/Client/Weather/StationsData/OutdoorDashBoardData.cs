using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Weather.StationsData
{
    public class OutdoorDashBoardData : DashBoardData
    {
        [JsonProperty("Humidity")]
        public int HumidityPercent { get; set; }

        [JsonProperty("Temperature")]
        public double Temperature { get; set; }

        // temp_trend for last 12h (up, down, stable)
        [JsonProperty("temp_trend")]
        public string TempTrend { get; set; }

        [JsonProperty("min_temp")]
        public double MinTemp { get; set; }

        [JsonProperty("max_temp")]
        public double MaxTemp { get; set; }

        [JsonProperty("date_min_temp")]
        public LocalDateTime MinTempAt { get; set; }

        [JsonProperty("date_max_temp")]
        public LocalDateTime MaxTempAt { get; set; }
    }
}