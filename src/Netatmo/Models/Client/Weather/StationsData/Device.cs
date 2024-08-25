using System.Text.Json.Serialization;
using Netatmo.Enums;
using Netatmo.Models.Client.Weather.StationsData.DashboardData;
using NodaTime;

namespace Netatmo.Models.Client.Weather.StationsData;

public class Device
{
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [JsonPropertyName("cipher_id")]
    public string CipherId { get; set; }

    [JsonPropertyName("station_name")]
    public string StationName { get; set; }

    // NAMain: Base station, NAModule1: Outdoor Module, NAModule2: Wind Gauge, NAModule3: Rain Gauge, NAModule4: Optional indoor module
    [JsonPropertyName("type")]
    public string Type { get; set; }

    // Wifi signal quality : 56 Good, 71 Average, 86 Bad
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

    [JsonPropertyName("module_name")]
    public string ModuleName { get; set; }

    [JsonPropertyName("co2_calibrating")]
    public bool Co2Calibrating { get; set; }

    [JsonPropertyName("firmware")]
    public int Firmware { get; set; }

    [JsonPropertyName("date_setup")]
    public Instant SetupAt { get; set; }

    [JsonPropertyName("last_setup")]
    public Instant LastSetupAt { get; set; }

    [JsonPropertyName("last_status_store")]
    public Instant LastStatusStoreAt { get; set; }

    [JsonPropertyName("dashboard_data")]
    public BaseStationDashBoardData DashboardData { get; set; }

    [JsonPropertyName("data_type")]
    public string[] DataType { get; set; }

    [JsonPropertyName("place")]
    public Place Place { get; set; }

    [JsonPropertyName("modules")]
    public Module[] Modules { get; set; }
}