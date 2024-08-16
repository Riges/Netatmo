using Newtonsoft.Json;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData;

public class RainGaugeDashBoardData : DashBoardData, IRainDashBoardData
{
    [JsonProperty("Rain")]
    public double Rain { get; set; }

    [JsonProperty("sum_rain_1")]
    public double RainLastHour { get; set; }

    [JsonProperty("sum_rain_24")]
    public double RainLastDay { get; set; }
}