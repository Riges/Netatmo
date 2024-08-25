using System.Text.Json.Serialization;
using NodaTime;

namespace Netatmo.Models.Client;

public record DataResponse(
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("time_exec")] double? TimeExec,
    [property: JsonPropertyName("time_server")] Instant? TimeServer);

public record DataResponse<T>(string Status, double? TimeExec, Instant? TimeServer, [property: JsonPropertyName("body")] T Body)
    : DataResponse(Status, TimeExec, TimeServer);