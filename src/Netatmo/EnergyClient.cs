using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Netatmo.Models.Client;
using Netatmo.Models.Client.Energy;

namespace Netatmo
{
    public class EnergyClient : IEnergyClient
    {
        private readonly string baseUrl;
        private readonly ICredentialManager credentialManager;

        public EnergyClient(string baseUrl, ICredentialManager credentialManager)
        {
            this.credentialManager = credentialManager;
            this.baseUrl = baseUrl;
        }

        public Task<DataResponse<GetHomesDataBody>> GetHomesData(string homeId = null, string gatewayTypes = null)
        {
            return baseUrl
                .AppendPathSegment("/api/homesdata")
                .WithOAuthBearerToken(credentialManager.AccessToken)
                .PostJsonAsync(new GetHomesDataRequest
                {
                    AccessToken = credentialManager.AccessToken,
                    HomeId = homeId,
                    GatewayTypes = gatewayTypes
                })
                .ReceiveJson<DataResponse<GetHomesDataBody>>();
        }
    }
}