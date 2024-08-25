using System.Text.Json.Serialization;

namespace Netatmo.Models.Client.Energy.HomesData;

public class Room
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("measure_offset_NAPlug_estimated_temperature")]
    public double MeasureOffsetNaPlugEstimatedTemperature { get; set; }

    [JsonPropertyName("measure_offset_NAPlug_temperature")]
    public double MeasureOffsetNaPlugTemperature { get; set; }

    [JsonPropertyName("module_ids")]
    public string[] ModuleIds { get; set; }
}