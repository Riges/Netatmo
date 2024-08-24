using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Energy;

public record CreateHomeScheduleResponse(
    string Status,
    double? TimeExec,
    Instant? TimeServer,
    [property: JsonProperty("schedule_id")]
    string ScheduleId) : DataResponse(Status, TimeExec, TimeServer);