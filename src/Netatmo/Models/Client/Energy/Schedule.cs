using Newtonsoft.Json;

namespace Netatmo.Models.Client.Energy
{
    public class Schedule
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        // Away temperature value
        [JsonProperty("away_temp")]
        public double AwayTemp { get; set; }

        // Frostguard temperature value
        [JsonProperty("hg_temp")]
        public double HgTemp { get; set; }

        [JsonProperty("selected")]
        public bool Selected { get; set; }

        [JsonProperty("timetables")]
        public Timetable[] Timetables { get; set; }

        [JsonProperty("zones")]
        public Zone[] Zones { get; set; }
    }
}