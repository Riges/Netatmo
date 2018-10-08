using Netatmo.Converters;
using Netatmo.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NodaTime;

namespace Netatmo.Models.Client.Weather.StationsData
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

        public RfStrengthEnum RfStrength
        {
            get
            {
                if (RfStatus <= 60) return RfStrengthEnum.FullSignal;

                if (RfStatus <= 70) return RfStrengthEnum.High;

                if (RfStatus <= 80) return RfStrengthEnum.Medium;

                return RfStrengthEnum.Low;
            }
        }

        // Percentage of battery remaining (10=low)
        [JsonProperty("battery_percent")]
        public int BatteryPercent { get; set; }

        [JsonProperty("battery_vp")]
        public int BatteryVp { get; set; }

        public BatteryLevelEnum BatteryStatus
        {
            get
            {
                switch (Type)
                {
                    case "NAModule2" when BatteryVp >= 6000:
                        return BatteryLevelEnum.Max;
                    case "NAModule2" when BatteryVp >= 5590:
                        return BatteryLevelEnum.Full;
                    case "NAModule2" when BatteryVp >= 5180:
                        return BatteryLevelEnum.High;
                    case "NAModule2" when BatteryVp >= 4770:
                        return BatteryLevelEnum.Medium;
                    case "NAModule2" when BatteryVp >= 4360:
                        return BatteryLevelEnum.Low;
                    case "NAModule2":
                        return BatteryLevelEnum.VeryLow;
                    case "NAModule4" when BatteryVp >= 6000:
                        return BatteryLevelEnum.Max;
                    case "NAModule4" when BatteryVp >= 5640:
                        return BatteryLevelEnum.Full;
                    case "NAModule4" when BatteryVp >= 5280:
                        return BatteryLevelEnum.High;
                    case "NAModule4" when BatteryVp >= 4920:
                        return BatteryLevelEnum.Medium;
                    case "NAModule4" when BatteryVp >= 4560:
                        return BatteryLevelEnum.Low;
                    case "NAModule4":
                        return BatteryLevelEnum.VeryLow;
                    case "NAModule1" when BatteryVp >= 6000:
                    case "NAModule3" when BatteryVp >= 6000:
                        return BatteryLevelEnum.Max;
                    case "NAModule1" when BatteryVp >= 5500:
                    case "NAModule3" when BatteryVp >= 5500:
                        return BatteryLevelEnum.Full;
                    case "NAModule1" when BatteryVp >= 5000:
                    case "NAModule3" when BatteryVp >= 5000:
                        return BatteryLevelEnum.High;
                    case "NAModule1" when BatteryVp >= 4500:
                    case "NAModule3" when BatteryVp >= 4500:
                        return BatteryLevelEnum.Medium;
                    case "NAModule1" when BatteryVp >= 4000:
                    case "NAModule3" when BatteryVp >= 4000:
                        return BatteryLevelEnum.Low;
                    case "NAModule1":
                    case "NAModule3":
                        return BatteryLevelEnum.VeryLow;
                    default:
                        return BatteryLevelEnum.Undefined;
                }
            }
        }

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