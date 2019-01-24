using System.IO;
using System.Threading.Tasks;
using Flurl.Http;
using Netatmo.Models.Client;
using Netatmo.Models.Client.Air;

namespace Netatmo
{
    public class AirClient : IAirClient
    {
        private readonly string baseUrl;
        private readonly ICredentialManager credentialManager;

        public AirClient(string baseUrl, ICredentialManager credentialManager)
        {
            this.baseUrl = baseUrl;
            this.credentialManager = credentialManager;
        }

        public Task<DataResponse<GetHomeCoachsData>> GetHomeCoachsData()
        {
            return baseUrl
                .ConfigureRequest(Configuration.ConfigureRequest)
                .AppendPathSegment("/api/gethomecoachsdata")
                .WithOAuthBearerToken(credentialManager.AccessToken)
                .PostJsonAsync(new { })
                .ReceiveJson<DataResponse<GetHomeCoachsData>>();
        }
    }
}