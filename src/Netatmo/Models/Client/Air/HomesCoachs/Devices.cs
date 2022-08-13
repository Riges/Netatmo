using Netatmo.Enums;
using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Air.HomesCoachs;

public class Devices
{
    [JsonProperty("_id")] 
    public string Id { get; set; }

    [JsonProperty("cipher_id")] 
    public string CipherId { get; set; }

    [JsonProperty("last_status_store")] 
    public Instant LastStatusStore { get; set; }

    [JsonProperty("place")] 
    public Place Place { get; set; }

    [JsonProperty("type")] 
    public string Type { get; set; }

    [JsonProperty("dashboard_data")] 
    public DashBoardData DashboardData { get; set; }

    [JsonProperty("data_type")] 
    public string[] DataType { get; set; }

    [JsonProperty("co2_calibrating")] 
    public bool Co2Calibrating { get; set; }

    [JsonProperty("reachable")] 
    public bool Reachable { get; set; }

    [JsonProperty("date_setup")] 
    public Instant DateSetup { get; set; }

    [JsonProperty("last_setup")] 
    public Instant LastSetup { get; set; }

    [JsonProperty("module_name")] 
    public string ModuleName { get; set; }

    [JsonProperty("firmware")] 
    public int Firmware { get; set; }

    [JsonProperty("last_upgrade")] 
    public Instant LastUpgrade { get; set; }
        
    [JsonProperty("station_name")] 
    public string Name { get; set; }

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

        
}