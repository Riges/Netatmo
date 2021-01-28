using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Flurl.Http.Testing;
using Moq;
using Netatmo.Enums;
using Netatmo.Models.Client.Air;
using NodaTime;
using Xunit;

namespace Netatmo.Tests
{
    public class AirClient : IDisposable
    {
        private readonly HttpTest httpTest;

        public AirClient()
        {
            httpTest = new HttpTest();
            httpTest.Configure(Configuration.ConfigureRequest);
        }

        public void Dispose()
        {
            httpTest.Dispose();
        }

        [Fact]
        public async Task GetStationsData_Should_Return_DataResponse_With_AirData()
        {
            var accessToken = "Super-Access-Token";
            var credentialManagerMock = new Mock<ICredentialManager>();
            credentialManagerMock.Setup(x => x.AccessToken).Returns(accessToken);

            httpTest.RespondWith(
                "{\"body\": {\"devices\": [{\"_id\": \"70:xx:xx:xx:xx:c2\",\"cipher_id\": \"enc:aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa28Zn\",\"date_setup\": 1548316279,\"last_setup\": 1548316279,\"type\": \"NHC\",\"last_status_store\": 1548331813,\"module_name\": \"Indoor\",\"firmware\": 45,\"last_upgrade\": 1548316280,\"wifi_status\": 49,\"reachable\": true,\"co2_calibrating\": true,\"station_name\": \"Healthy ConX\",\"data_type\": [\"Temperature\",\"CO2\",\"Humidity\",\"Noise\",\"Pressure\",\"health_idx\"],\"place\": {\"altitude\": 491.70001220703,\"country\": \"CH\",\"timezone\": \"Europe/Zurich\",\"location\": [8.3085069,47.0450306]},\"dashboard_data\": {\"time_utc\": 1548331811,\"Temperature\": 22.1,\"CO2\": 647,\"Humidity\": 33,\"Noise\": 44,\"Pressure\": 1018.3,\"AbsolutePressure\": 960.4,\"health_idx\": 0,\"min_temp\": 17.3,\"max_temp\": 23.6,\"date_min_temp\": 1548316040,\"date_max_temp\": 1548316058}}],\"user\": {\"mail\": \"a@mail.ch\",\"administrative\": {\"lang\": \"en-US\",\"reg_locale\": \"en-US\",\"country\": \"CH\",\"unit\": 0,\"windunit\": 0,\"pressureunit\": 0,\"feel_like_algo\": 0}}},\"status\": \"ok\",\"time_exec\": 0.038990020751953,\"time_server\": 1548331875}");

            var sut = new Netatmo.AirClient("https://api.netatmo.com/", credentialManagerMock.Object);
            var result = await sut.GetHomeCoachsData();

            httpTest
                .ShouldHaveCalled("https://api.netatmo.com/api/gethomecoachsdata")
                .WithVerb(HttpMethod.Post)
                .WithOAuthBearerToken(accessToken)
                .WithContentType("application/json").WithRequestJson(new GetHomeCoachsDataRequest())
                .Times(1);

            result.Body.Should().BeOfType<GetHomeCoachsData>();
            result.Body.Devices[0].DashboardData.Noise.Should().Be(44);
            result.Body.Devices[0].WifiStrength.Should().Be(WifiStrengthEnum.Good);
            result.Body.Devices[0].DashboardData.HumidityPercent.Should().Be(33);
            result.Body.Devices[0].Place.Timezone.Should().Be(DateTimeZoneProviders.Tzdb["Europe/Zurich"]);
        }
    }
}