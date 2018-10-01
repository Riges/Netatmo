using System.Threading.Tasks;

namespace Netatmo
{
    public interface IClient
    {
        IWeatherClient Weather { get; }
        IEnergyClient Energy { get; }
        ICredentialManager CredentialManager { get; }
        Task GenerateToken(string username, string password, Scope[] scopes = null);
        Task RefreshToken();
    }
}