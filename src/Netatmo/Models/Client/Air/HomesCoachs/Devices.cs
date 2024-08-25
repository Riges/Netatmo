using System.Text.Json.Serialization;
using Netatmo.Enums;
using NodaTime;

namespace Netatmo.Models.Client.Air.HomesCoachs;

public class Devices
{
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [JsonPropertyName("cipher_id")]
    public string CipherId { get; set; }

    [JsonPropertyName("last_status_store")]
    public Instant LastStatusStore { get; set; }

    [JsonPropertyName("place")]
    public Place Place { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("dashboard_data")]
    public DashBoardData DashboardData { get; set; }

    [JsonPropertyName("data_type")]
    public string[] DataType { get; set; }

    [JsonPropertyName("co2_calibrating")]
    public bool Co2Calibrating { get; set; }

    [JsonPropertyName("reachable")]
    public bool Reachable { get; set; }

    [JsonPropertyName("date_setup")]
    public Instant DateSetup { get; set; }

    [JsonPropertyName("last_setup")]
    public Instant LastSetup { get; set; }

    [JsonPropertyName("module_name")]
    public string ModuleName { get; set; }

    [JsonPropertyName("firmware")]
    public int Firmware { get; set; }

    [JsonPropertyName("last_upgrade")]
    public Instant LastUpgrade { get; set; }

    [JsonPropertyName("station_name")]
    public string Name { get; set; }

    [JsonPropertyName("wifi_status")]
    public int WifiStatus { get; set; }

    public WifiStrengthEnum WifiStrength
    {
        get
        {
            if (WifiStatus <= 56)
            {
                return WifiStrengthEnum.Good;
            }

            if (WifiStatus <= 71)
            {
                return WifiStrengthEnum.Average;
            }

            return WifiStrengthEnum.Bad;
        }
    }
}