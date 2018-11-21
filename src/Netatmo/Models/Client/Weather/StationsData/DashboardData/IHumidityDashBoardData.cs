using Newtonsoft.Json;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData
{
    public interface IHumidityDashBoardData : IDashBoardData
    {
        [JsonProperty("Humidity")]
        int HumidityPercent { get; set; }
    }
}