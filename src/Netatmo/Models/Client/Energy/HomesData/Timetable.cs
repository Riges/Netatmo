using Newtonsoft.Json;

namespace Netatmo.Models.Client.Energy.HomesData
{
    public class Timetable
    {
        [JsonProperty("id")]
        public string ZoneId { get; set; }

        // offset in minutes since Monday 00:00:01
        [JsonProperty("m_offset")]
        public int MOffset { get; set; }
    }
}