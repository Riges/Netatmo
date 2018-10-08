using System.Threading.Tasks;
using Netatmo.Models.Client;
using Netatmo.Models.Client.Energy;
using NodaTime;

namespace Netatmo
{
    public interface IEnergyClient
    {
        Task<DataResponse<GetHomesDataBody>> GetHomesData(string homeId = null, string gatewayTypes = null);
        Task<DataResponse<GetHomeStatusBody>> GetHomeStatus(string homeId, string[] deviceTypes = null);
        Task<bool> SetThermMode(string homeId, string mode, LocalDateTime? endTime = null);
        Task<bool> SetRoomThermPoint(string homeId, string roomId, string mode, double? temp = null, LocalDateTime? endTime = null);
    }
}