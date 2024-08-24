using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client;

public record DataResponse(
    [property: JsonProperty("status")] string Status,
    [property: JsonProperty("time_exec")] double? TimeExec,
    [property: JsonProperty("time_server")] Instant? TimeServer);

public record DataResponse<T>(
    string Status,
    double? TimeExec,
    Instant? TimeServer,
    [property: JsonProperty("body")] T Body)
    : DataResponse(Status, TimeExec, TimeServer);