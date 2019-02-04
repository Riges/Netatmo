using System;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Util;
using Netatmo;
using Netatmo.Models.Client.Energy;
using Netatmo.Models.Client.Energy.RoomMeasure;
using Newtonsoft.Json;
using NodaTime;

namespace TestApp
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            JsonConvert.DefaultSettings = Configuration.JsonSerializer;

            IClient client = new Client(
                SystemClock.Instance, " https://api.netatmo.com/",
                "5c4f159c6c320317008f1669",
                "RcE4HcD3Nypad3kM2xMAmY9DfPdIedAo3r3nN4aoiH");

            await client.GenerateToken(
                "5be083e50f21e10b008d47c3|7c5a6601ee054b1f6bdd1a7f0a979a31");

            var token = client.CredentialManager.CredentialToken;

            Console.WriteLine($"Token : {token.AccessToken}");

            Console.WriteLine("Stations data :");
            var stationsData = await client.Weather.GetStationsData();
            Console.WriteLine(JsonConvert.SerializeObject(stationsData, Formatting.Indented));

            Console.WriteLine("Energy Homes data :");
            var homesData = await client.Energy.GetHomesData();
            Console.WriteLine(JsonConvert.SerializeObject(homesData, Formatting.Indented));

            Console.WriteLine("Energy Homes data :");
            foreach (var home in homesData.Body.Homes)
            {
                Console.WriteLine(home.Name);
                var homeStatus = await client.Energy.GetHomeStatus(home.Id);
                Console.WriteLine(JsonConvert.SerializeObject(homeStatus, Formatting.Indented));

                Console.WriteLine("Energy room measure :");
                foreach (var room in home.Rooms)
                {
                    Console.WriteLine(room.Name);
                    var parameters = new GetRoomMeasureParameters
                    {
                        HomeId = home.Id,
                        RoomId = room.Id,
                        Scale = Scale.Max,
                        Type = ThermostatMeasurementType.Temperature,
                        BeginAt = SystemClock.Instance.GetCurrentInstant().Plus(Duration.FromDays(-1)),
                        EndAt = SystemClock.Instance.GetCurrentInstant()
                    };
                    var roomMeasure = await client.Energy.GetRoomMeasure<TemperatureStep>(parameters);
                    Console.WriteLine(JsonConvert.SerializeObject(roomMeasure, Formatting.Indented));
                }
            }

            Console.WriteLine("RefreshToken :");
            Thread.Sleep(9000);
            await client.RefreshToken();
            var newToken = client.CredentialManager.CredentialToken;
            Console.WriteLine($"Old token expires at : {token.ExpiresAt.ToInvariantString()}");
            Console.WriteLine($"New token expires at : {newToken.ExpiresAt.ToInvariantString()}");
        }
    }
}