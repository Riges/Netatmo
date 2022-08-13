using Newtonsoft.Json;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData;

public interface INoiseDashBoardData : IDashBoardData
{
    [JsonProperty("Noise")]
    double Noise { get; set; }
}