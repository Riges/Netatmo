using System.Threading.Tasks;
using Netatmo.Models.Client;
using Netatmo.Models.Client.Energy;
using Netatmo.Models.Client.Energy.RoomMeasure;
using NodaTime;

namespace Netatmo
{
    public interface IEnergyClient
    {
        Task<DataResponse<GetHomesDataBody>> GetHomesData(string homeId = null, string gatewayTypes = null);
        Task<DataResponse<GetHomeStatusBody>> GetHomeStatus(string homeId, string[] deviceTypes = null);
        Task<DataResponse> SetThermMode(string homeId, string mode, LocalDateTime? endTime = null);
        Task<DataResponse> SetRoomThermPoint(string homeId, string roomId, string mode, double? temp = null, LocalDateTime? endTime = null);
        Task<DataResponse<T[]>> GetRoomMeasure<T>(GetRoomMeasureParameters parameters) where T : IStep;
        Task<DataResponse> SwitchHomeSchedule(string homeId, string scheduleId);
    }
}