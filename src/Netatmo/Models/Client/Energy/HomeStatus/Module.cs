using System.Text.Json.Serialization;
using Netatmo.Enums;

namespace Netatmo.Models.Client.Energy.HomeStatus;

public class Module
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    // NATherm1 = thermostat, NRV = valve, NAPlug = relay, NACamera = welcome camera, NOC = presence camera
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("bridge")]
    public string Bridgereachable { get; set; }

    // Wifi signal quality : 56 Good, 71 Average, 86 Bad
    [JsonPropertyName("wifi_strength")]
    public int? WifiStrength { get; set; }

    public WifiStrengthEnum WifiStatus
    {
        get
        {
            if (!WifiStrength.HasValue)
            {
                return WifiStrengthEnum.Undefined;
            }

            if (WifiStrength.Value <= 56)
            {
                return WifiStrengthEnum.Good;
            }

            if (WifiStrength.Value <= 71)
            {
                return WifiStrengthEnum.Average;
            }

            return WifiStrengthEnum.Bad;
        }
    }

    // Radio signal quality : 90 = low, 80 = medium, 70 = high, 60 = full signal
    [JsonPropertyName("rf_strength")]
    public int RfStrength { get; set; }

    public RfStrengthEnum RfStatus
    {
        get
        {
            if (RfStrength <= 60)
            {
                return RfStrengthEnum.FullSignal;
            }

            if (RfStrength <= 70)
            {
                return RfStrengthEnum.High;
            }

            if (RfStrength <= 80)
            {
                return RfStrengthEnum.Medium;
            }

            return RfStrengthEnum.Low;
        }
    }

    // Only for NATherm1
    [JsonPropertyName("connected_to_boiler")]
    public bool? ConnectedToBoiler { get; set; }

    // Only for NATherm1
    [JsonPropertyName("boiler_status")]
    public bool? BoilerStatus { get; set; }

    // Only for NATherm1
    [JsonPropertyName("boiler_valve_comfort_boost")]
    public bool? BoilerValveComfortBoost { get; set; }

    [JsonPropertyName("battery_level")]
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

    [JsonPropertyName("battery_state")]
    public string BatteryState { get; set; }

    [JsonPropertyName("firmware_revision")]
    public int FirmwareRevision { get; set; }

    // Only for valve type Number displayed during the pairing with the relay
    [JsonPropertyName("radio_id")]
    public int? RadioId { get; set; }

    [JsonPropertyName("anticipating")]
    public bool? Anticipating { get; set; }

    [JsonPropertyName("reachable")]
    public bool? Reachable { get; set; }
}