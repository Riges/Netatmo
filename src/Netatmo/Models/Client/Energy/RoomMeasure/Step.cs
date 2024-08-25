using System.Text.Json.Serialization;
using NodaTime;

namespace Netatmo.Models.Client.Energy.RoomMeasure;

public abstract class Step<T> : IStep
{
    [JsonPropertyName("beg_time")]
    public Instant BeginAt { get; set; }

    [JsonPropertyName("step_time")]
    public int StepTime { get; set; }

    [JsonPropertyName("value")]
    public T[][] Values { get; set; }
}