using Newtonsoft.Json;

namespace Netatmo.Models.Client.Energy
{
    public class Timetable
    {
        public Timetable()
        {
        }

        public Timetable(string zoneId, int mOffset)
        {
            ZoneId = zoneId;
            MOffset = mOffset;
        }

        [JsonProperty("id")]
        public string ZoneId { get; set; }

        // offset in minutes since Monday 00:00:01
        [JsonProperty("m_offset")]
        public int MOffset { get; set; }
    }
}