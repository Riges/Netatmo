using System.Threading.Tasks;
using NodaTime;

namespace Netatmo
{
    public class Client : IClient
    {
        public Client(IClock clock, string baseUrl, string clientId, string clientSecret)
        {
            CredentialManager = new CredentialManager(baseUrl, clientId, clientSecret, clock);
            Weather = new WeatherClient(baseUrl, CredentialManager);
            Energy = new EnergyClient(baseUrl, CredentialManager);
            Air = new AirClient(baseUrl, CredentialManager);
        }

        public IWeatherClient Weather { get; }
        public IEnergyClient Energy { get; }
        public IAirClient Air { get; }
        public ICredentialManager CredentialManager { get; }

        public Task GenerateToken(string username, string password, Scope[] scopes = null)
        {
            return CredentialManager.GenerateToken(username, password, scopes);
        }
        
        public Task GenerateToken(string oauth2Token)
        {
            return CredentialManager.GenerateToken(oauth2Token);
        }

        public Task RefreshToken()
        {
            return CredentialManager.RefreshToken();
        }
    }
}