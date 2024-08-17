using Flurl.Http;
using Netatmo.Models.Client;
using Netatmo.Models.Client.Energy;
using Netatmo.Models.Client.Energy.RoomMeasure;
using NodaTime;

namespace Netatmo;

public class EnergyClient(string baseUrl, ICredentialManager credentialManager) : IEnergyClient
{
    public Task<DataResponse<GetHomesDataBody>> GetHomesData(string homeId = null, string gatewayTypes = null) =>
        baseUrl.ConfigureRequest(Configuration.ConfigureRequest)
            .AppendPathSegment("/api/homesdata")
            .WithOAuthBearerToken(credentialManager.AccessToken)
            .PostJsonAsync(new GetHomesDataRequest { HomeId = homeId, GatewayTypes = gatewayTypes })
            .ReceiveJson<DataResponse<GetHomesDataBody>>();

    public async Task<DataResponse<GetHomeStatusBody>> GetHomeStatus(string homeId, string[] deviceTypes = null) =>
        await baseUrl.ConfigureRequest(Configuration.ConfigureRequest)
            .AppendPathSegment("/api/homestatus")
            .WithOAuthBearerToken(credentialManager.AccessToken)
            .PostJsonAsync(new GetHomeStatusRequest { HomeId = homeId, DeviceTypes = deviceTypes })
            .ReceiveJson<DataResponse<GetHomeStatusBody>>();

    public async Task<DataResponse> SetThermMode(string homeId, string mode, Instant? endTime = null) =>
        await baseUrl.ConfigureRequest(Configuration.ConfigureRequest)
            .AppendPathSegment("/api/setthermmode")
            .WithOAuthBearerToken(credentialManager.AccessToken)
            .PostJsonAsync(new SetThermModeRequest { HomeId = homeId, Mode = mode, EndTime = endTime })
            .ReceiveJson<DataResponse>();

    public async Task<DataResponse> SetRoomThermPoint(string homeId, string roomId, string mode, double? temp = null, Instant? endTime = null) =>
        await baseUrl.ConfigureRequest(Configuration.ConfigureRequest)
            .AppendPathSegment("/api/setroomthermpoint")
            .WithOAuthBearerToken(credentialManager.AccessToken)
            .PostJsonAsync(
                new SetRoomThermpointRequest
                {
                    HomeId = homeId,
                    RoomId = roomId,
                    Mode = mode,
                    Temp = temp,
                    EndTime = endTime
                })
            .ReceiveJson<DataResponse>();

    public async Task<DataResponse<T[]>> GetRoomMeasure<T>(GetRoomMeasureParameters parameters)
        where T : IStep
    {
        ValidateGetRoomMeasureParameters<T>(parameters);

        return await baseUrl.ConfigureRequest(Configuration.ConfigureRequest)
            .AppendPathSegment("/api/getroommeasure")
            .WithOAuthBearerToken(credentialManager.AccessToken)
            .PostJsonAsync(
                new GetRoomMeasureRequest
                {
                    HomeId = parameters.HomeId,
                    RoomId = parameters.RoomId,
                    Scale = parameters.Scale.Value,
                    Type = parameters.Type.Value,
                    BeginAt = parameters.BeginAt,
                    EndAt = parameters.EndAt,
                    Limit = parameters.Limit,
                    Optimize = parameters.Optimize,
                    RealTime = parameters.RealTime
                })
            .ReceiveJson<DataResponse<T[]>>();
    }

    public async Task<DataResponse> SwitchHomeSchedule(string homeId, string scheduleId) =>
        await baseUrl.ConfigureRequest(Configuration.ConfigureRequest)
            .AppendPathSegment("/api/switchhomeschedule")
            .WithOAuthBearerToken(credentialManager.AccessToken)
            .PostJsonAsync(new SwitchHomeScheduleRequest { HomeId = homeId, ScheduleId = scheduleId })
            .ReceiveJson<DataResponse>();

    public async Task<DataResponse> RenameHomeSchedule(string homeId, string scheduleId, string name) =>
        await baseUrl.ConfigureRequest(Configuration.ConfigureRequest)
            .AppendPathSegment("/api/renamehomeschedule")
            .WithOAuthBearerToken(credentialManager.AccessToken)
            .PostJsonAsync(new RenameHomeScheduleRequest { HomeId = homeId, ScheduleId = scheduleId, Name = name })
            .ReceiveJson<DataResponse>();

    public async Task<DataResponse> DeleteHomeSchedule(string homeId, string scheduleId) =>
        await baseUrl.ConfigureRequest(Configuration.ConfigureRequest)
            .AppendPathSegment("/api/deletehomeschedule")
            .WithOAuthBearerToken(credentialManager.AccessToken)
            .PostJsonAsync(new DeleteHomeScheduleRequest { HomeId = homeId, ScheduleId = scheduleId })
            .ReceiveJson<DataResponse>();

    public async Task<DataResponse> SyncHomeSchedule(SyncHomeScheduleRequest requestParameters) =>
        await baseUrl.ConfigureRequest(Configuration.ConfigureRequest)
            .AppendPathSegment("/api/synchomeschedule")
            .WithOAuthBearerToken(credentialManager.AccessToken)
            .PostJsonAsync(requestParameters)
            .ReceiveJson<DataResponse>();

    public async Task<CreateHomeScheduleResponse> CreateHomeSchedule(CreateHomeScheduleRequest requestParameters) =>
        await baseUrl.ConfigureRequest(Configuration.ConfigureRequest)
            .AppendPathSegment("/api/createnewhomeschedule")
            .WithOAuthBearerToken(credentialManager.AccessToken)
            .PostJsonAsync(requestParameters)
            .ReceiveJson<CreateHomeScheduleResponse>();

    private void ValidateGetRoomMeasureParameters<T>(GetRoomMeasureParameters parameters)
    {
        if (string.IsNullOrWhiteSpace(parameters.HomeId))
        {
            throw new ArgumentException("Home Id shouldn't be null");
        }

        if (string.IsNullOrWhiteSpace(parameters.RoomId))
        {
            throw new ArgumentException("Room Id shouldn't be null");
        }

        if (parameters.Scale == null)
        {
            throw new ArgumentException("Scale shouldn't be null");
        }

        if (parameters.Type == null)
        {
            throw new ArgumentException("Type shouldn't be null");
        }

        if (ThermostatMeasurementType.AvailableTypes(parameters.Scale).All(type => type.Value != parameters.Type.Value))
        {
            throw new ArgumentException("Type shouldn't be allow for this scale");
        }

        if (parameters.Limit.HasValue && (parameters.Limit.Value < 0 || parameters.Limit.Value > 1024))
        {
            throw new ArgumentException("Limit should be between 0 and 1024");
        }

        if (parameters.BeginAt.HasValue && parameters.EndAt.HasValue && parameters.BeginAt.Value > parameters.EndAt.Value)
        {
            throw new ArgumentException("BeginAt should be lower than EndAt");
        }

        if (parameters.Type == ThermostatMeasurementType.Temperature
            || parameters.Type == ThermostatMeasurementType.SetPointTemperature
            || parameters.Type == ThermostatMeasurementType.MinTemp
            || parameters.Type == ThermostatMeasurementType.MaxTemp)
        {
            if (typeof(T) != typeof(TemperatureStep))
            {
                throw new ArgumentException("TemperatureStep should be used with a temperature measurement");
            }
        }
        else if (parameters.Type == ThermostatMeasurementType.DateMinTemp)
        {
            if (typeof(T) != typeof(DateTemperatureStep))
            {
                throw new ArgumentException("DateTemperatureStep should be used with a date of temperature measurement");
            }
        }
    }
}