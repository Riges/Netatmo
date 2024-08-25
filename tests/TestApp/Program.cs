using System.Text.Json;
using Flurl.Http;
using Netatmo;
using Netatmo.Models.Client.Energy;
using Netatmo.Models.Client.Energy.RoomMeasure;
using NodaTime;

var jsonSerializerOptions = Configuration.JsonSerializerOptions;
jsonSerializerOptions.WriteIndented = true;


var client = new Client(
    SystemClock.Instance,
    "https://api.netatmo.com/",
    Environment.GetEnvironmentVariable("NETATMO_CLIENT_ID"),
    Environment.GetEnvironmentVariable("NETATMO_CLIENT_SECRET"));

// TODO - We use Generated Token, we need to rework how GenerateToken work for test it.
client.ProvideOAuth2Token(Environment.GetEnvironmentVariable("NETATMO_CLIENT_TOKEN"), Environment.GetEnvironmentVariable("NETATMO_CLIENT_TOKEN_REFRESH"));
// await client.GenerateToken(
//     Environment.GetEnvironmentVariable("NETATMO_USERNAME"),
//     Environment.GetEnvironmentVariable("NETATMO_PASSWORD"),
//     [
//         Scope.CameraAccess,
//         Scope.CameraRead,
//         Scope.CameraWrite,
//         Scope.HomecoachRead,
//         Scope.PresenceAccess,
//         Scope.PresenceRead,
//         Scope.StationRead,
//         Scope.StationWrite,
//         Scope.ThermostatRead
//     ]);

var token = client.CredentialManager.CredentialToken;

Console.WriteLine($"Token : {token.AccessToken}");

Console.WriteLine("Stations data :");
var stationsData = await client.Weather.GetStationsData();
Console.WriteLine(JsonSerializer.Serialize(stationsData, jsonSerializerOptions));

Console.WriteLine("Energy Homes data :");
var homesData = await client.Energy.GetHomesData();
Console.WriteLine(JsonSerializer.Serialize(homesData, jsonSerializerOptions));

Console.WriteLine("Energy Homes data :");
foreach (var home in homesData.Body.Homes)
{
    Console.WriteLine(home.Name);
    var homeStatus = await client.Energy.GetHomeStatus(home.Id);
    Console.WriteLine(JsonSerializer.Serialize(homeStatus, jsonSerializerOptions));

    Console.WriteLine("Energy room measure :");
    foreach (var room in home.Rooms)
    {
        if (room.ModuleIds == null || room.ModuleIds.Length == 0)
        {
            continue;
        }

        Console.WriteLine(room.Name);
        var parameters = new GetRoomMeasureParameters
        {
            HomeId = home.Id,
            RoomId = room.Id,
            Scale = Scale.OneMonth,
            Type = ThermostatMeasurementType.Temperature,
            BeginAt = SystemClock.Instance.GetCurrentInstant().Plus(Duration.FromDays(-1)),
            EndAt = SystemClock.Instance.GetCurrentInstant()
        };

        try
        {
            var roomMeasure = await client.Energy.GetRoomMeasure<TemperatureStep>(parameters);
            Console.WriteLine(JsonSerializer.Serialize(roomMeasure, jsonSerializerOptions));
        }
        catch (FlurlHttpException exception)
        {
            var error = await exception.GetResponseStringAsync();
            Console.WriteLine($"exception : {error}");
        }
    }
}

// TODO - We don' t refresh the token because for now we use generated token
// Console.WriteLine("RefreshToken :");
// Thread.Sleep(9000);

// await client.RefreshToken();
// var newToken = client.CredentialManager.CredentialToken;
// Console.WriteLine($"Old token expires at : {token.ExpiresAt.ToInvariantString()}");
// Console.WriteLine($"New token expires at : {newToken.ExpiresAt.ToInvariantString()}");