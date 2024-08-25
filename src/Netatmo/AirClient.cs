using Flurl.Http;
using Netatmo.Models.Client;
using Netatmo.Models.Client.Air;

namespace Netatmo;

public class AirClient(string baseUrl, ICredentialManager credentialManager) : IAirClient
{
    public Task<DataResponse<GetHomeCoachsData>> GetHomeCoachsData(string deviceId = null) =>
        baseUrl.WithSettings(Configuration.ConfigureRequest)
            .AppendPathSegment("/api/gethomecoachsdata")
            .WithOAuthBearerToken(credentialManager.AccessToken)
            .PostJsonAsync(new GetHomeCoachsDataRequest { DeviceId = deviceId })
            .ReceiveJson<DataResponse<GetHomeCoachsData>>();
}