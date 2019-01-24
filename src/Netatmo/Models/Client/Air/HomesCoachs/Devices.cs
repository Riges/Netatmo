using Netatmo.Models.Client.Weather.StationsData;
using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Air.HomesCoachs
{
    public class Devices
    {
        [JsonProperty("_id")] 
        string Id { get; set; }

        [JsonProperty("cipher_id")] 
        string CipherId { get; set; }

        [JsonProperty("last_status_store")] 
        Instant LastStatusStore { get; set; }

        [JsonProperty("modules")] 
        dynamic[] Modules { get; set; }

        [JsonProperty("place")] 
        Place Place { get; set; }

        [JsonProperty("type")] 
        string Type { get; set; }

        [JsonProperty("dashboard_data")] 
        DashBoardData DashboardData { get; set; }

        [JsonProperty("data_type")] 
        string[] DataType { get; set; }

        [JsonProperty("co2_calibrating")] 
        bool Co2Calibrating { get; set; }

        [JsonProperty("reachable")] 
        bool Reachable { get; set; }

        [JsonProperty("date_setup")] 
        Instant DateSetup { get; set; }

        [JsonProperty("last_setup")] 
        Instant LastSetup { get; set; }

        [JsonProperty("module_name")] 
        string ModuleName { get; set; }

        [JsonProperty("firmware")] 
        int Firmware { get; set; }

        [JsonProperty("last_upgrade")] 
        Instant LastUpgrade { get; set; }

        [JsonProperty("wifi_status")] 
        int WifiStatus { get; set; }

        [JsonProperty("name")] 
        string Name { get; set; }
    }
}