using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData
{
    public interface ITemperatureDashBoardData : IDashBoardData
    {
        [JsonProperty("Temperature")]
        double Temperature { get; set; }

        // temp_trend for last 12h (up, down, stable)
        [JsonProperty("temp_trend")]
        string TempTrend { get; set; }

        [JsonProperty("min_temp")]
        double MinTemp { get; set; }

        [JsonProperty("max_temp")]
        double MaxTemp { get; set; }

        [JsonProperty("date_min_temp")]
        Instant MinTempAt { get; set; }

        [JsonProperty("date_max_temp")]
        Instant MaxTempAt { get; set; }
    }
}