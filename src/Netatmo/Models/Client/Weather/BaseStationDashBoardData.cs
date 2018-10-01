using Netatmo.Converters;
using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Weather
{
    public class BaseStationDashBoardData : DashBoardData
    {
        [JsonProperty("AbsolutePressure")]
        public double AbsolutePressure { get; set; }

        [JsonProperty("CO2")]
        public int CO2 { get; set; }

        [JsonProperty("Humidity")]
        public int HumidityPercent { get; set; }

        [JsonProperty("Noise")]
        public double Noise { get; set; }

        [JsonProperty("Pressure")]
        public double Pressure { get; set; }

        // pressure_trend for last 12h (up, down, stable)
        [JsonProperty("pressure_trend")]
        public string PressureTrend { get; set; }

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
        [JsonConverter(typeof(TimestampToLocalDateTimeConverter))]
        public LocalDateTime MinTempAt { get; set; }

        [JsonProperty("date_max_temp")]
        [JsonConverter(typeof(TimestampToLocalDateTimeConverter))]
        public LocalDateTime MaxTempAt { get; set; }
    }
}