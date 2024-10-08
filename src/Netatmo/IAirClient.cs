using Netatmo.Models.Client;
using Netatmo.Models.Client.Air;

namespace Netatmo;

public interface IAirClient
{
    Task<DataResponse<GetHomeCoachsData>> GetHomeCoachsData(string deviceId = null);
}