using System.Threading.Tasks;

namespace Netatmo
{
    public interface IClient
    {
        IWeatherClient Weather { get; }
        IEnergyClient Energy { get; }
        IAirClient Air { get; }
        ICredentialManager CredentialManager { get; }
        Task GenerateToken(string username, string password, Scope[] scopes = null);
        void ProvideOAuth2Token(string oauth2Token);
        Task RefreshToken();
    }
}