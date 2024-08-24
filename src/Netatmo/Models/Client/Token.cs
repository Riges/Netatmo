using Newtonsoft.Json;

namespace Netatmo.Models.Client;

public record Token(
    [property: JsonProperty("expires_in")] int ExpiresIn,
    [property: JsonProperty("access_token")]
    string AccessToken,
    [property: JsonProperty("refresh_token")]
    string RefreshToken);