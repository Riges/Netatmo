using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Flurl.Http.Testing;
using Netatmo.Models;
using Netatmo.Models.Client;
using Netatmo.Models.Client.Weather;
using NodaTime;
using NodaTime.Testing;
using Xunit;

namespace Netatmo.Tests
{
    public class Client : IDisposable
    {
        public Client()
        {
            httpTest = new HttpTest();
        }

        public void Dispose()
        {
            httpTest.Dispose();
        }

        private readonly HttpTest httpTest;

        [Fact]
        public async Task GetStationsDataShouldReturnDataResponseWithStationsData()
        {
            var accessToken = "Super-Access-Token";
            httpTest.RespondWith(
                "{\"body\":{\"devices\":[{\"_id\":\"70:ee:50:17:d7:68\",\"cipher_id\":\"enc:16:d7oXslmrLcm69wuZl2klZAy1PDEXgL81jOzEZlC6MyPSyoG1Sv+ipU\\/tlJbfzWzK\",\"date_setup\":1452883523,\"last_setup\":1452883523,\"type\":\"NAMain\",\"last_status_store\":1534855959,\"module_name\":\"Station Maison\",\"firmware\":135,\"wifi_status\":50,\"co2_calibrating\":false,\"station_name\":\"TDD1 - Netatmo\",\"data_type\":[\"Temperature\",\"CO2\",\"Humidity\",\"Noise\",\"Pressure\"],\"place\":{\"altitude\":46,\"city\":\"Vitry-sur-Seine\",\"country\":\"FR\",\"timezone\":\"Europe\\/Paris\",\"location\":[2.40372,48.770259]},\"dashboard_data\":{\"time_utc\":1534855927,\"Temperature\":26.1,\"CO2\":460,\"Humidity\":57,\"Noise\":42,\"Pressure\":1022.1,\"AbsolutePressure\":1016.6,\"min_temp\":25.6,\"max_temp\":27,\"date_min_temp\":1534853808,\"date_max_temp\":1534842291,\"temp_trend\":\"stable\",\"pressure_trend\":\"stable\"},\"modules\":[{\"_id\":\"02:00:00:17:ec:42\",\"type\":\"NAModule1\",\"module_name\":\"Module Balcon\",\"data_type\":[\"Temperature\",\"Humidity\"],\"last_setup\":1452883525,\"dashboard_data\":{\"time_utc\":1534855900,\"Temperature\":26.2,\"Humidity\":47,\"min_temp\":22.6,\"max_temp\":26.2,\"date_min_temp\":1534826831,\"date_max_temp\":1534855900,\"temp_trend\":\"stable\"},\"firmware\":44,\"last_message\":1534855957,\"last_seen\":1534855951,\"rf_status\":74,\"battery_vp\":5876,\"battery_percent\":95},{\"_id\":\"03:00:00:02:7b:d4\",\"type\":\"NAModule4\",\"module_name\":\"Chambre\",\"data_type\":[\"Temperature\",\"CO2\",\"Humidity\"],\"last_setup\":1463504629,\"dashboard_data\":{\"time_utc\":1534855900,\"Temperature\":26.2,\"CO2\":484,\"Humidity\":54,\"min_temp\":23,\"max_temp\":26.8,\"date_min_temp\":1534805300,\"date_max_temp\":1534834113,\"temp_trend\":\"stable\"},\"firmware\":44,\"last_message\":1534855958,\"last_seen\":1534855951,\"rf_status\":68,\"battery_vp\":6001,\"battery_percent\":100}]}],\"user\":{\"mail\":\"netatmo@jarvis-system.net\",\"administrative\":{\"country\":\"FR\",\"reg_locale\":\"fr-FR\",\"lang\":\"fr-FR\",\"unit\":0,\"windunit\":0,\"pressureunit\":0,\"feel_like_algo\":0}}},\"status\":\"ok\",\"time_exec\":0.12225985527039,\"time_server\":1534856201}");

            var sut = new Netatmo.Client(SystemClock.Instance, "https://api.netatmo.com/", "clientId", "clientSecret");
            var result = await sut.GetStationsData(accessToken);
            
            httpTest
                .ShouldHaveCalled("https://api.netatmo.com/api/getstationsdata")
                .WithVerb(HttpMethod.Post)
                .WithContentType("application/json").WithRequestJson(new GetStationsDataRequest
                {
                    AccessToken = accessToken
                })
                .Times(1);

            result.Body.Should().BeOfType<GetStationsDataBody>();
            result.Body.Devices[0].DashboardData.Noise.Should().Be(42);
            result.Body.Devices[0].Modules[1].DashboardData.ToObject<IndoorDashBoardData>().CO2.Should().Be(484);
            result.Body.Devices[0].Place.Timezone.Should().Be(DateTimeZoneProviders.Tzdb["Europe/Paris"]);
        }

        [Fact]
        public async Task GetTokenShouldReturnExceptedCredentialToken()
        {
            var expectedToken = new
            {
                access_token = "2YotnFZFEjr1zCsicMWpAA",
                expires_in = 10800,
                refresh_token = "tGzv3JOkF0XG5Qx2TlKWIA"
            };

            httpTest.RespondWithJson(expectedToken);

            var sut = new Netatmo.Client(SystemClock.Instance, "https://api.netatmo.com/", "clientId", "clientSecret");

            var result = await sut.GetToken("username@email.com", "p@$$W0rd",
                new[]
                {
                    Scope.CameraAccess, Scope.CameraRead, Scope.CameraWrite, Scope.HomecoachRead, Scope.PresenceAccess, Scope.PresenceRead,
                    Scope.StationRead, Scope.StationWrite, Scope.ThermostatRead
                });

            httpTest
                .ShouldHaveCalled("https://api.netatmo.com/oauth2/token")
                .WithVerb(HttpMethod.Post)
                .WithContentType("application/x-www-form-urlencoded")
                .WithRequestBody(
                    "grant_type=password&client_id=clientId&client_secret=clientSecret&username=username%40email.com&password=p%40%24%24W0rd&scope=access_camera+read_camera+write_camera+read_homecoach+access_presence+read_presence+read_station+write_thermostat+read_thermostat")
                .Times(1);

            result.Should().BeOfType<CredentialToken>();
            result.AccessToken.Should().Be(expectedToken.access_token);
            result.RefreshToken.Should().Be(expectedToken.refresh_token);
            result.ExpiresAt.Should().Be(result.ReceivedAt.Plus(Duration.FromSeconds(expectedToken.expires_in)));
        }

        [Fact]
        public async Task RefreshTokenShouldRefreshTheToken()
        {
            var token = new Token {ExpiresIn = 60, AccessToken = "2YotnFZFEjr1zCsicMWpAA", RefreshToken = "tGzv3JOkF0XG5Qx2TlKWIA"};
            var credentialToken = new CredentialToken(token, new FakeClock(SystemClock.Instance.GetCurrentInstant(), Duration.FromMinutes(-2)));

            var expectedToken = new
            {
                access_token = "dGVzdGNsaWVudDpzZWNyZXQ",
                expires_in = 10800,
                refresh_token = "fdb8fdbecf1d03ce5e6125c067733c0d51de209c"
            };

            httpTest.RespondWithJson(expectedToken);

            var sut = new Netatmo.Client(SystemClock.Instance, "https://api.netatmo.com/", "clientId", "clientSecret");

            var result = await sut.RefreshToken(credentialToken);

            httpTest
                .ShouldHaveCalled("https://api.netatmo.com/oauth2/token")
                .WithVerb(HttpMethod.Post)
                .WithContentType("application/x-www-form-urlencoded")
                .WithRequestBody(
                    $"grant_type=refresh_token&client_id=clientId&client_secret=clientSecret&refresh_token={token.RefreshToken}")
                .Times(1);

            result.Should().BeOfType<CredentialToken>();
            result.AccessToken.Should().Be(expectedToken.access_token);
            result.RefreshToken.Should().Be(expectedToken.refresh_token);
            result.ExpiresAt.Should().Be(result.ReceivedAt.Plus(Duration.FromSeconds(expectedToken.expires_in)));
        }
    }
}