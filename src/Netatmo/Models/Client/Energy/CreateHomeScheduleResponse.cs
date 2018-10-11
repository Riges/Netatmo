using Newtonsoft.Json;

namespace Netatmo.Models.Client.Energy
{
    public class CreateHomeScheduleResponse : DataResponse
    {
        [JsonProperty("schedule_id")]
        public string ScheduleId { get; set; }
    }
}