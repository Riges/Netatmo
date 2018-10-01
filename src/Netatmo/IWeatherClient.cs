using System.Threading.Tasks;
using Netatmo.Models.Client;
using Netatmo.Models.Client.Weather;

namespace Netatmo
{
    public interface IWeatherClient
    {
        Task<DataResponse<GetStationsDataBody>> GetStationsData(string deviceId = null, bool? onlyFavorites = null);
    }
}