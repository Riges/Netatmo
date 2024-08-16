using Newtonsoft.Json;

namespace Netatmo.Models.Client.Energy;

public class CreateHomeScheduleRequest
{
    public CreateHomeScheduleRequest()
    {
        Timetables = new List<Timetable>();
        Zones = new List<Zone>();
    }

    public CreateHomeScheduleRequest(string homeId, double hgTemp, double awayTemp, string name, Timetable[] timetables = null,
        Zone[] zones = null) : 
        this()
    {
        HomeId = homeId;
        Name = name;
        HgTemp = hgTemp;
        AwayTemp = awayTemp;

        if (timetables != null) Timetables.AddRange(timetables);

        if (zones != null) Zones.AddRange(zones);
    }

    [JsonProperty("home_id")]
    public string HomeId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("hg_temp")]
    public double HgTemp { get; set; }

    [JsonProperty("away_temp")]
    public double AwayTemp { get; set; }

    [JsonProperty("timetable")]
    public List<Timetable> Timetables { get; set; }

    [JsonProperty("zones")]
    public List<Zone> Zones { get; set; }
}