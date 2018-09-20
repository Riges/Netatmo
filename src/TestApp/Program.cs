using System;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Util;
using Netatmo;
using Netatmo.Models.Client;
using Newtonsoft.Json;
using NodaTime;

namespace TestApp
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var client = new Client(
                SystemClock.Instance, "https://api.netatmo.com/",
                Environment.GetEnvironmentVariable("NETATMO_CLIENT_ID"),
                Environment.GetEnvironmentVariable("NETATMO_CLIENT_SECRET"));

            var token = await client.GetToken(
                Environment.GetEnvironmentVariable("NETATMO_USERNAME"),
                Environment.GetEnvironmentVariable("NETATMO_PASSWORD"));

            Console.WriteLine("Stations data :");
            var stationsData = await client.GetStationsData(token.AccessToken);
            Console.WriteLine(JsonConvert.SerializeObject(stationsData, Formatting.Indented));
            
            
            Console.WriteLine("RefreshToken :");
            Thread.Sleep(9000);
            var newToken = await client.RefreshToken(token);
            Console.WriteLine($"Old token expires at : {token.ExpiresAt.ToInvariantString()}");
            Console.WriteLine($"New token expires at : {newToken.ExpiresAt.ToInvariantString()}");
            
            Console.WriteLine("Press any key to quit...");
            Console.ReadKey();
        }
    }
}