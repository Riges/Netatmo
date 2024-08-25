using System.Text.Json.Serialization;
using Netatmo.Enums;

namespace Netatmo.Models.Client.Energy.HomesData;

public class User
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("language")]
    public string Language { get; set; }

    [JsonPropertyName("locale")]
    public string Locale { get; set; }

    [JsonPropertyName("feel_like_algorithm")]
    public FeelLikeAlgoEnum FeelLikeAlgorithm { get; set; }

    [JsonPropertyName("unit_pressure")]
    public PressureUnitEnum PressureUnit { get; set; }

    [JsonPropertyName("unit_system")]
    public UnitEnum Unit { get; set; }

    [JsonPropertyName("unit_wind")]
    public WindUnitEnum WindUnit { get; set; }
}