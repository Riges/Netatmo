using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Weather.StationsData
{
    public class DashBoardData : IDashBoardData
    {
        [JsonProperty("time_utc")]
        public Instant TimeUtc { get; set; }
    }

    public class OutdoorDashBoardData : DashBoardData, ITemperatureDashBoardData, IHumidityDashBoardData
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
        public Instant MinTempAt { get; set; }

        [JsonProperty("date_max_temp")]
        public Instant MaxTempAt { get; set; }
    }

    public class IndoorDashBoardData : DashBoardData, ITemperatureDashBoardData, ICO2DashBoardData
    {
        [JsonProperty("CO2")]
        public int CO2 { get; set; }

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
        public Instant MinTempAt { get; set; }

        [JsonProperty("date_max_temp")]
        public Instant MaxTempAt { get; set; }
    }

    public class RainGaugeDashBoardData : DashBoardData, IRainDashBoardData
    {
        [JsonProperty("Rain")]
        public double Rain { get; set; }

        [JsonProperty("sum_rain_1")]
        public double RainLastHour { get; set; }

        [JsonProperty("sum_rain_24")]
        public double RainLastDay { get; set; }
    }

    public class WindGaugeDashBoardData : DashBoardData, IWindDashBoardData, IGustDashBoardData
    {
        [JsonProperty("WindHistoric")]
        public WindHistoric[] WindHistoric { get; set; }

        [JsonProperty("GustStrength")]
        public int GustStrength { get; set; }

        [JsonProperty("GustAngle")]
        public int GustAngle { get; set; }

        [JsonProperty("WindStrength")]
        public int WindStrength { get; set; }

        [JsonProperty("WindAngle")]
        public int WindAngle { get; set; }
    }
}