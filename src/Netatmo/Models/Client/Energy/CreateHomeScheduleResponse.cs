using System.Text.Json.Serialization;
using NodaTime;

namespace Netatmo.Models.Client.Energy;

public record CreateHomeScheduleResponse(string Status, double? TimeExec, Instant? TimeServer, [property: JsonPropertyName("schedule_id")] string ScheduleId)
    : DataResponse(Status, TimeExec, TimeServer);