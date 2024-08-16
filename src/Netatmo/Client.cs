using NodaTime;

namespace Netatmo;

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
        Console.WriteLine("Client credentials grant type is deprecated since october 2022 and will not work!");return CredentialManager.GenerateToken(username, password, scopes);
    }
        
    public void ProvideOAuth2Token(string accessToken)
        {
            CredentialManager.ProvideOAuth2Token(accessToken);
        }
        
        public void ProvideOAuth2Token(string accessToken, string refreshToken)
    {
        CredentialManager.ProvideOAuth2Token(accessToken, refreshToken);
    }

    public Task RefreshToken()
    {
        return CredentialManager.RefreshToken();
    }
}