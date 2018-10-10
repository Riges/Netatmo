using Newtonsoft.Json;

namespace Netatmo.Models.Client.Energy
{
    public class DeleteHomeScheduleRequest
    {
        [JsonProperty("home_id")]
        public string HomeId { get; set; }

        [JsonProperty("schedule_id")]
        public string ScheduleId { get; set; }
    }
}