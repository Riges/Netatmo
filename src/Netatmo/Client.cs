using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Netatmo.Models;
using Netatmo.Models.Client;
using Netatmo.Models.Client.Weather;
using NodaTime;

namespace Netatmo
{
    public class Client
    {
        private readonly string baseUrl;
        private readonly string clientId;
        private readonly string clientSecret;
        private readonly IClock clock;

        public Client(IClock clock, string baseUrl, string clientId, string clientSecret)
        {
            this.baseUrl = baseUrl;
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            this.clock = clock;
        }

        public async Task<CredentialToken> GetToken(string username, string password, Scope[] scopes = null)
        {
            var scope = string.Join(" ", scopes?.Select(s => s.Value) ?? new string[0]);

            // TODO : Handle not success status codes (rate limit exceeded, api down, ect)
            var token = await baseUrl.AppendPathSegment("/oauth2/token").PostUrlEncodedAsync(new
            {
                grant_type = "password",
                client_id = clientId,
                client_secret = clientSecret,
                username,
                password,
                scope
            }).ReceiveJson<Token>();

            return new CredentialToken(token, clock);
        }

        public async Task<CredentialToken> RefreshToken(CredentialToken credentialToken)
        {
            // TODO : Handle not success status codes (rate limit exceeded, api down, ect)
            var token = await baseUrl.AppendPathSegment("/oauth2/token").PostUrlEncodedAsync(new
            {
                grant_type = "refresh_token",
                client_id = clientId,
                client_secret = clientSecret,
                refresh_token = credentialToken.RefreshToken
            }).ReceiveJson<Token>();

            return new CredentialToken(token, clock);
        }
        
        public async Task<DataResponse<GetStationsDataBody>> GetStationsData(string accessToken, string deviceId = null, bool? onlyFavorites = null)
        {
            return await baseUrl
                .AppendPathSegment("/api/getstationsdata")
                .PostJsonAsync(new GetStationsDataRequest
                {
                    AccessToken = accessToken,
                    DeviceId = deviceId,
                    GetFavorites = onlyFavorites
                })
                .ReceiveJson<DataResponse<GetStationsDataBody>>();
        }
    }
}