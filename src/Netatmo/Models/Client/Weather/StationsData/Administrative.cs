using System.Text.Json.Serialization;
using Netatmo.Enums;

namespace Netatmo.Models.Client.Weather.StationsData;

public class Administrative
{
    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("feel_like_algo")]
    public FeelLikeAlgoEnum FeelLikeAlgo { get; set; }

    [JsonPropertyName("lang")]
    public string Lang { get; set; }

    [JsonPropertyName("pressureunit")]
    public PressureUnitEnum PressureUnit { get; set; }

    [JsonPropertyName("reg_locale")]
    public string RegLocale { get; set; }

    [JsonPropertyName("unit")]
    public UnitEnum Unit { get; set; }

    [JsonPropertyName("windunit")]
    public WindUnitEnum WindUnit { get; set; }
}