using System.Text.Json.Serialization;

namespace Netatmo.Models.Client.Weather.StationsData.DashboardData;

public interface IWindHistory : IDashBoardData
{
    [JsonPropertyName("WindStrength")]
    int WindStrength { get; set; }

    [JsonPropertyName("WindAngle")]
    int WindAngle { get; set; }
}

public interface IWindDashBoardData : IWindHistory
{
    WindHistoric[] WindHistoric { get; set; }
}