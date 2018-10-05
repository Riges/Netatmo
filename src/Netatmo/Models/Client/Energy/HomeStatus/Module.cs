using Netatmo.Models.Client.Enums;
using Newtonsoft.Json;

namespace Netatmo.Models.Client.Energy.HomeStatus
{
    public class Module
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        // NATherm1 = thermostat, NRV = valve, NAPlug = relay, NACamera = welcome camera, NOC = presence camera
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("bridge")]
        public string Bridgereachable { get; set; }

        // Wifi signal quality : 56 Good, 71 Average, 86 Bad
        [JsonProperty("wifi_strength")]
        public int? WifiStrength { get; set; }

        public WifiStrengthEnum WifiStatus
        {
            get
            {
                if (!WifiStrength.HasValue) return WifiStrengthEnum.Undefined;

                if (WifiStrength.Value <= 56) return WifiStrengthEnum.Good;

                if (WifiStrength.Value <= 71) return WifiStrengthEnum.Average;

                return WifiStrengthEnum.Bad;
            }
        }

        // Radio signal quality : 90 = low, 80 = medium, 70 = high, 60 = full signal
        [JsonProperty("rf_strength")]
        public int RfStrength { get; set; }

        public RfStrengthEnum RfStatus
        {
            get
            {
                if (RfStrength <= 60) return RfStrengthEnum.FullSignal;

                if (RfStrength <= 70) return RfStrengthEnum.High;

                if (RfStrength <= 80) return RfStrengthEnum.Medium;

                return RfStrengthEnum.Low;
            }
        }

        // Only for NATherm1
        [JsonProperty("connected_to_boiler")]
        public bool? ConnectedToBoiler { get; set; }

        // Only for NATherm1
        [JsonProperty("boiler_status")]
        public bool? BoilerStatus { get; set; }

        // Only for NATherm1
        [JsonProperty("boiler_valve_comfort_boost")]
        public bool? BoilerValveComfortBoost { get; set; }
        
        [JsonProperty("battery_level")]
        public int BatteryLevel { get; set; }

        public BatteryLevelEnum BatteryStatus
        {
            get
            {
                switch (Type)
                {
                    case "NATherm1" when BatteryLevel >= 4100:
                        return BatteryLevelEnum.Full;
                    case "NATherm1" when BatteryLevel >= 3600:
                        return BatteryLevelEnum.High;
                    case "NATherm1" when BatteryLevel >= 3300:
                        return BatteryLevelEnum.Medium;
                    case "NATherm1":
                        return BatteryLevelEnum.Low;
                    case "NRV" when BatteryLevel >= 3200:
                        return BatteryLevelEnum.Full;
                    case "NRV" when BatteryLevel >= 2700:
                        return BatteryLevelEnum.High;
                    case "NRV" when BatteryLevel >= 2400:
                        return BatteryLevelEnum.Medium;
                    case "NRV":
                        return BatteryLevelEnum.Low;
                    default:
                        return BatteryLevelEnum.Undefined;
                }
            }
        }
        
        [JsonProperty("battery_state")]
        public string BatteryState { get; set; }

        [JsonProperty("firmware_revision")]
        public int FirmwareRevision { get; set; }

        // Only for valve type Number displayed during the pairing with the relay
        [JsonProperty("radio_id")]
        public int? RadioId { get; set; }

        [JsonProperty("anticipating")]
        public bool? Anticipating { get; set; }

        [JsonProperty("reachable")]
        public bool? Reachable { get; set; }
    }
}