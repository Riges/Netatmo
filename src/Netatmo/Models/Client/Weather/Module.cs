using Netatmo.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NodaTime;

namespace Netatmo.Models.Client.Weather
{
    public class Module
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        // NAMain: Base station, NAModule1: Outdoor Module, NAModule2: Wind Gauge, NAModule3: Rain Gauge, NAModule4: Optional indoor module
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("module_name")]
        public string ModuleName { get; set; }

        // Current radio status per module. (90=low, 60=highest)
        [JsonProperty("rf_status")]
        public int RfStatus { get; set; }

        // Percentage of battery remaining (10=low)
        [JsonProperty("battery_percent")]
        public int BatteryPercent { get; set; }

        [JsonProperty("battery_vp")]
        public int BatteryVp { get; set; }

        [JsonProperty("firmware")]
        public int Firmware { get; set; }

        [JsonProperty("last_message")]
        [JsonConverter(typeof(TimestampToLocalDateTimeConverter))]
        public LocalDateTime LastMessageAt { get; set; }

        [JsonProperty("last_seen")]
        [JsonConverter(typeof(TimestampToLocalDateTimeConverter))]
        public LocalDateTime LastSeenAt { get; set; }

        [JsonProperty("last_setup")]
        [JsonConverter(typeof(TimestampToLocalDateTimeConverter))]
        public LocalDateTime LastSetupAt { get; set; }

        [JsonProperty("data_type")]
        public string[] DataType { get; set; }

        [JsonProperty("dashboard_data")]
        public JObject DashboardData { get; set; }
    }
}