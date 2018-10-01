using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Netatmo.Models.Client;
using Netatmo.Models.Client.Weather;

namespace Netatmo
{
    public class WeatherClient : IWeatherClient
    {
        private readonly string baseUrl;
        private readonly ICredentialManager credentialManager;

        public WeatherClient(string baseUrl, ICredentialManager credentialManager)
        {
            this.credentialManager = credentialManager;
            this.baseUrl = baseUrl;
        }

        public Task<DataResponse<GetStationsDataBody>> GetStationsData(string deviceId = null, bool? onlyFavorites = null)
        {
            return baseUrl
                .AppendPathSegment("/api/getstationsdata")
                .WithOAuthBearerToken(credentialManager.AccessToken)
                .PostJsonAsync(new GetStationsDataRequest
                {
                    AccessToken = credentialManager.AccessToken,
                    DeviceId = deviceId,
                    GetFavorites = onlyFavorites
                })
                .ReceiveJson<DataResponse<GetStationsDataBody>>();
        }
    }
}