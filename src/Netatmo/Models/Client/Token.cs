using System.Text.Json.Serialization;

namespace Netatmo.Models.Client;

public record Token(
    [property: JsonPropertyName("expires_in")] int ExpiresIn,
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("refresh_token")] string RefreshToken);