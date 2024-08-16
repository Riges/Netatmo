using Netatmo.Models;

namespace Netatmo;

public interface ICredentialManager
{
    CredentialToken CredentialToken { get; }
    string AccessToken { get; }
    Task GenerateToken(string username, string password, Scope[] scopes = null);
    void ProvideOAuth2Token(string accessToken);
        void ProvideOAuth2Token(string accessToken, string refreshToken);
    Task RefreshToken();
}