using Newtonsoft.Json;

namespace Netatmo.Models.Client.Energy
{
    public class RenameHomeScheduleRequest
    {
        [JsonProperty("home_id")]
        public string HomeId { get; set; }

        [JsonProperty("schedule_id")]
        public string ScheduleId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}