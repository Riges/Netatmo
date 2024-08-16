using Flurl;
using Flurl.Http;
using Netatmo.Models;
using Netatmo.Models.Client;
using NodaTime;

namespace Netatmo;

public class CredentialManager : ICredentialManager
{
    private readonly string baseUrl;
    private readonly string clientId;
    private readonly string clientSecret;
    private readonly IClock clock;

    public CredentialManager(string baseUrl, string clientId, string clientSecret, IClock clock)
    {
        this.baseUrl = baseUrl;
        this.clientId = clientId;
        this.clientSecret = clientSecret;
        this.clock = clock;
    }

    public CredentialToken CredentialToken { get; private set; }
    public string AccessToken => CredentialToken?.AccessToken;

    public async Task GenerateToken(string username, string password, Scope[] scopes = null)
    {
        var scope = string.Join(" ", scopes?.Select(s => s.Value) ?? new string[0]);

        // TODO : Handle not success status codes (rate limit exceeded, api down, ect)
        var token = await baseUrl.AppendPathSegment("/oauth2/token")
            .PostUrlEncodedAsync(
                new
                {
                    grant_type = "password",
                    client_id = clientId,
                    client_secret = clientSecret,
                    username,
                    password,
                    scope
                })
            .ReceiveJson<Token>();

        CredentialToken = new CredentialToken(token, clock);
    }

    public void ProvideOAuth2Token(string accessToken, string refreshToken)
    {
        CredentialToken = new CredentialToken(new Token(20, accessToken, refreshToken), clock);
    }

    public void ProvideOAuth2Token(string accessToken)
    {
        ProvideOAuth2Token(accessToken, null);
    }

    public async Task RefreshToken()
    {
        // TODO : Handle not success status codes (rate limit exceeded, api down, ect)
        var token = await baseUrl.AppendPathSegment("/oauth2/token")
            .PostUrlEncodedAsync(new { grant_type = "refresh_token", client_id = clientId, client_secret = clientSecret, refresh_token = CredentialToken.RefreshToken })
            .ReceiveJson<Token>();

        CredentialToken = new CredentialToken(token, clock);
    }
}