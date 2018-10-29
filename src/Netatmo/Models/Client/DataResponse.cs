using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client
{
    public class DataResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("time_exec")]
        public double? TimeExec { get; set; }

        [JsonProperty("time_server")]
        public Instant? TimeServer { get; set; }
    }

    public class DataResponse<T> : DataResponse
    {
        [JsonProperty("body")]
        public T Body { get; set; }
    }
}