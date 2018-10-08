using System;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Netatmo.Models.Client;
using Netatmo.Models.Client.Energy;
using NodaTime;

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
                    HomeId = homeId,
                    GatewayTypes = gatewayTypes
                })
                .ReceiveJson<DataResponse<GetHomesDataBody>>();
        }

        public async Task<DataResponse<GetHomeStatusBody>> GetHomeStatus(string homeId, string[] deviceTypes = null)
        {
            return await baseUrl
                .AppendPathSegment("/api/homestatus")
                .WithOAuthBearerToken(credentialManager.AccessToken)
                .PostJsonAsync(new GetHomeStatusRequest
                {
                    HomeId = homeId,
                    DeviceTypes = deviceTypes
                })
                .ReceiveJson<DataResponse<GetHomeStatusBody>>();
        }

        public async Task<bool> SetThermMode(string homeId, string mode, LocalDateTime? endTime = null)
        {
            var response = await baseUrl
                .AppendPathSegment("/api/setroomthermmode")
                .WithOAuthBearerToken(credentialManager.AccessToken)
                .PostJsonAsync(new SetThermModeRequest
                {
                    HomeId = homeId,
                    Mode = mode,
                    EndTime = endTime
                }).ReceiveJson<DataResponse>();

            return response.Status.Equals("ok", StringComparison.OrdinalIgnoreCase);
        }

        public async Task<bool> SetRoomThermPoint(string homeId, string roomId, string mode, double? temp = null, LocalDateTime? endTime = null)
        {
            var response = await baseUrl
                .AppendPathSegment("/api/setroomthermpoint")
                .WithOAuthBearerToken(credentialManager.AccessToken)
                .PostJsonAsync(new SetRoomThermpointRequest
                {
                    HomeId = homeId,
                    RoomId = roomId,
                    Mode = mode,
                    Temp = temp,
                    EndTime = endTime
                }).ReceiveJson<DataResponse>();

            return response.Status.Equals("ok", StringComparison.OrdinalIgnoreCase);
        }
    }
}