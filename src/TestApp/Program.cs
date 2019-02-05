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
                "5be083e50f21e10b008d47c3|881bb37c6553f9255f7b0173d342e0e1");

            var token = client.CredentialManager.CredentialToken;

            Console.WriteLine($"Token : {token.AccessToken}");

            Console.WriteLine("Stations data :");
            var stationsData = await client.Air.GetHomeCoachsData();
            Console.WriteLine(JsonConvert.SerializeObject(stationsData, Formatting.Indented));


            Console.WriteLine("RefreshToken :");
            Thread.Sleep(9000);
            await client.RefreshToken();
            var newToken = client.CredentialManager.CredentialToken;
            Console.WriteLine($"Old token expires at : {token.ExpiresAt.ToInvariantString()}");
            Console.WriteLine($"New token expires at : {newToken.ExpiresAt.ToInvariantString()}");
        }
    }
}