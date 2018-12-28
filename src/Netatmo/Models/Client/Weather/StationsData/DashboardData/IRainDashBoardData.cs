using Newtonsoft.Json;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData
{
    public interface IRainDashBoardData : IDashBoardData
    {
        [JsonProperty("Rain")]
        double Rain { get; set; }

        [JsonProperty("sum_rain_1")]
        double RainLastHour { get; set; }

        [JsonProperty("sum_rain_24")]
        double RainLastDay { get; set; }
    }
}