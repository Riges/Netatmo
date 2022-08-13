using Netatmo.Enums;
using Netatmo.Models.Client.Weather.StationsData.DashboardData;
using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Weather.StationsData;

public class Device
{
    [JsonProperty("_id")]
    public string Id { get; set; }

    [JsonProperty("cipher_id")]
    public string CipherId { get; set; }

    [JsonProperty("station_name")]
    public string StationName { get; set; }

    // NAMain: Base station, NAModule1: Outdoor Module, NAModule2: Wind Gauge, NAModule3: Rain Gauge, NAModule4: Optional indoor module
    [JsonProperty("type")]
    public string Type { get; set; }

    // Wifi signal quality : 56 Good, 71 Average, 86 Bad
    [JsonProperty("wifi_status")]
    public int WifiStatus { get; set; }

    public WifiStrengthEnum WifiStrength
    {
        get
        {
            if (WifiStatus <= 56) return WifiStrengthEnum.Good;

            if (WifiStatus <= 71) return WifiStrengthEnum.Average;

            return WifiStrengthEnum.Bad;
        }
    }

    [JsonProperty("module_name")]
    public string ModuleName { get; set; }

    [JsonProperty("co2_calibrating")]
    public bool Co2Calibrating { get; set; }

    [JsonProperty("firmware")]
    public int Firmware { get; set; }

    [JsonProperty("date_setup")]
    public Instant SetupAt { get; set; }

    [JsonProperty("last_setup")]
    public Instant LastSetupAt { get; set; }

    [JsonProperty("last_status_store")]
    public Instant LastStatusStoreAt { get; set; }

    [JsonProperty("dashboard_data")]
    public BaseStationDashBoardData DashboardData { get; set; }

    [JsonProperty("data_type")]
    public string[] DataType { get; set; }

    [JsonProperty("place")]
    public Place Place { get; set; }

    [JsonProperty("modules")]
    public Module[] Modules { get; set; }
}