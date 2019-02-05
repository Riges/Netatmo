using System.Threading.Tasks;
using Netatmo.Models;

namespace Netatmo
{
    public interface ICredentialManager
    {
        CredentialToken CredentialToken { get; }
        string AccessToken { get; }
        Task GenerateToken(string username, string password, Scope[] scopes = null);
        Task GenerateToken(string oauth2Token);
        Task RefreshToken();
    }
}