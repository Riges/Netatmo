using Newtonsoft.Json;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData;

public interface IWindHistory : IDashBoardData
{
    [JsonProperty("WindStrength")]
    int WindStrength { get; set; }

    [JsonProperty("WindAngle")]
    int WindAngle { get; set; }
}

public interface IWindDashBoardData : IWindHistory
{
    WindHistoric[] WindHistoric { get; set; }
}