using System.Collections.Generic;
using Newtonsoft.Json;

namespace Netatmo.Models.Client.Energy
{
    public class SyncHomeScheduleRequest
    {
        public SyncHomeScheduleRequest()
        {
            Timetables = new List<Timetable>();
            Zones = new List<Zone>();
        }

        public SyncHomeScheduleRequest(string homeId, string scheduleId, double hgTemp, double awayTemp, string name = null,
            Timetable[] timetables = null, Zone[] zones = null) : this()
        {
            HomeId = homeId;
            ScheduleId = scheduleId;
            Name = name;
            HgTemp = hgTemp;
            AwayTemp = awayTemp;

            if (timetables != null) Timetables.AddRange(timetables);

            if (zones != null) Zones.AddRange(zones);
        }

        public SyncHomeScheduleRequest(string homeId, string scheduleId, double hgTemp, double awayTemp, string name) : this(homeId, scheduleId,
            hgTemp, awayTemp, name, null, null)
        {
        }

        public SyncHomeScheduleRequest(string homeId, string scheduleId, double hgTemp, double awayTemp, Timetable[] timetables, Zone[] zones) : this(
            homeId, scheduleId, hgTemp, awayTemp, null, timetables, zones)
        {
        }

        [JsonProperty("home_id")]
        public string HomeId { get; set; }

        [JsonProperty("schedule_id")]
        public string ScheduleId { get; set; }

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
}