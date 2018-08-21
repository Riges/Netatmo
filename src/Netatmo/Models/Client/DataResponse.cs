using Newtonsoft.Json;

namespace Netatmo.Models.Client
{
    public class DataResponse<T>
    {
        [JsonProperty("body")]
        public T Body { get; set; }
        
        [JsonProperty("status")]
        public string Status { get; set; }
        
        [JsonProperty("time_exec")]
        public double? TimeExec { get; set; }
        
        [JsonProperty("time_server")]
        public int? TimeServer { get; set; }
    }
}