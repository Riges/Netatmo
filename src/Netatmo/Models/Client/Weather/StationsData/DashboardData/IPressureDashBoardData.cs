using Newtonsoft.Json;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData
{
    public interface IPressureDashBoardData : IDashBoardData
    {
        [JsonProperty("AbsolutePressure")]
        double AbsolutePressure { get; set; }

        [JsonProperty("Pressure")]
        double Pressure { get; set; }

        // pressure_trend for last 12h (up, down, stable)
        [JsonProperty("pressure_trend")]
        string PressureTrend { get; set; }
    }
}