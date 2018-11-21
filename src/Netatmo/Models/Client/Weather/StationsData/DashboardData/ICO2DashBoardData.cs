using Newtonsoft.Json;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData
{
    public interface ICO2DashBoardData : IDashBoardData
    {
        [JsonProperty("CO2")]
        int CO2 { get; set; }
    }
}