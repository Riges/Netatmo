using System.Text.Json.Serialization;

namespace Netatmo.Models.Client.Energy;

public class CreateHomeScheduleRequest()
{
    public CreateHomeScheduleRequest(string homeId, double hgTemp, double awayTemp, string name, Timetable[] timetables = null, Zone[] zones = null)
        : this()
    {
        HomeId = homeId;
        Name = name;
        HgTemp = hgTemp;
        AwayTemp = awayTemp;

        if (timetables != null)
        {
            Timetables.AddRange(timetables);
        }

        if (zones != null)
        {
            Zones.AddRange(zones);
        }
    }

    [JsonPropertyName("home_id")]
    public string HomeId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("hg_temp")]
    public double HgTemp { get; set; }

    [JsonPropertyName("away_temp")]
    public double AwayTemp { get; set; }

    [JsonPropertyName("timetable")]
    public List<Timetable> Timetables { get; set; } = [];

    [JsonPropertyName("zones")]
    public List<Zone> Zones { get; set; } = [];
}