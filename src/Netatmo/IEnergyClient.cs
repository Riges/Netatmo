using System.Threading.Tasks;
using Netatmo.Models.Client;
using Netatmo.Models.Client.Energy;

namespace Netatmo
{
    public interface IEnergyClient
    {
        Task<DataResponse<GetHomesDataBody>> GetHomesData(string homeId = null, string gatewayTypes = null);
    }
}