using Netatmo.Enums;
using Newtonsoft.Json;

namespace Netatmo.Models.Client.Energy.HomesData;

public class User
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("language")]
    public string Language { get; set; }

    [JsonProperty("locale")]
    public string Locale { get; set; }

    [JsonProperty("feel_like_algorithm")]
    public FeelLikeAlgoEnum FeelLikeAlgorithm { get; set; }

    [JsonProperty("unit_pressure")]
    public PressureUnitEnum PressureUnit { get; set; }

    [JsonProperty("unit_system")]
    public UnitEnum Unit { get; set; }

    [JsonProperty("unit_wind")]
    public WindUnitEnum WindUnit { get; set; }
}