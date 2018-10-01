using System;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Util;
using Netatmo;
using Newtonsoft.Json;
using NodaTime;

namespace TestApp
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            IClient client = new Client(
                SystemClock.Instance, " https://api.netatmo.com/",
                Environment.GetEnvironmentVariable("NETATMO_CLIENT_ID"),
                Environment.GetEnvironmentVariable("NETATMO_CLIENT_SECRET"));

            await client.GenerateToken(
                Environment.GetEnvironmentVariable("NETATMO_USERNAME"),
                Environment.GetEnvironmentVariable("NETATMO_PASSWORD"),
                new[]
                {
                    Scope.CameraAccess, Scope.CameraRead, Scope.CameraWrite, Scope.HomecoachRead, Scope.PresenceAccess, Scope.PresenceRead,
                    Scope.StationRead, Scope.StationWrite, Scope.ThermostatRead
                });

            var token = client.CredentialManager.CredentialToken;

            Console.WriteLine("Stations data :");
            var stationsData = await client.Weather.GetStationsData();
            Console.WriteLine(JsonConvert.SerializeObject(stationsData, Formatting.Indented));

            Console.WriteLine("Energy Homes data :");
            var homesData = await client.Energy.GetHomesData();
            Console.WriteLine(JsonConvert.SerializeObject(homesData, Formatting.Indented));

            Console.WriteLine("RefreshToken :");
            Thread.Sleep(9000);
            await client.RefreshToken();
            var newToken = client.CredentialManager.CredentialToken;
            Console.WriteLine($"Old token expires at : {token.ExpiresAt.ToInvariantString()}");
            Console.WriteLine($"New token expires at : {newToken.ExpiresAt.ToInvariantString()}");
        }
    }
}