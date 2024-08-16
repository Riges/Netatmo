using Flurl.Http;
using Netatmo.Models.Client;
using Netatmo.Models.Client.Weather;

namespace Netatmo;

public class WeatherClient(string baseUrl, ICredentialManager credentialManager) : IWeatherClient
{
    public Task<DataResponse<GetStationsDataBody>> GetStationsData(string deviceId = null, bool? onlyFavorites = null) =>
        baseUrl.ConfigureRequest(Configuration.ConfigureRequest)
            .AppendPathSegment("/api/getstationsdata")
            .WithOAuthBearerToken(credentialManager.AccessToken)
            .PostJsonAsync(new GetStationsDataRequest { DeviceId = deviceId, GetFavorites = onlyFavorites })
            .ReceiveJson<DataResponse<GetStationsDataBody>>();
}