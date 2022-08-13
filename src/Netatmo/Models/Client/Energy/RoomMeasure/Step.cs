using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Energy.RoomMeasure;

public abstract class Step<T> : IStep
{
    [JsonProperty("beg_time")]
    public Instant BeginAt { get; set; }

    [JsonProperty("step_time")]
    public int StepTime { get; set; }

    [JsonProperty("value")]
    public T[][] Values { get; set; }
}