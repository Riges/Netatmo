using System.Text.Json.Serialization;

namespace Netatmo.Models.Client.Energy.HomesData;

public class Schedule
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    // Away temperature value
    [JsonPropertyName("away_temp")]
    public double AwayTemp { get; set; }

    // Frostguard temperature value
    [JsonPropertyName("hg_temp")]
    public double HgTemp { get; set; }

    [JsonPropertyName("selected")]
    public bool Selected { get; set; }

    [JsonPropertyName("timetables")]
    public Timetable[] Timetables { get; set; }

    [JsonPropertyName("zones")]
    public Zone[] Zones { get; set; }
}