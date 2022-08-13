using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Flurl.Http.Testing;
using Moq;
using Netatmo.Enums;
using Netatmo.Models.Client.Weather;
using Netatmo.Models.Client.Weather.StationsData.DashboardData;
using NodaTime;
using Xunit;

namespace Netatmo.Tests
{
    public class WeatherClient : IDisposable
    {
        private readonly HttpTest httpTest;

        public WeatherClient()
        {
            httpTest = new HttpTest();
            httpTest.Configure(Configuration.ConfigureRequest);
        }

        public void Dispose()
        {
            httpTest.Dispose();
        }

        [Fact]
        public async Task GetStationsData_Should_Return_DataResponse_With_StationsData()
        {
            var accessToken = "Super-Access-Token";
            var credentialManagerMock = new Mock<ICredentialManager>();
            credentialManagerMock.Setup(x => x.AccessToken).Returns(accessToken);

            httpTest.RespondWith(
                "{\"body\":{\"devices\":[{\"_id\":\"70:ee:50:2c:xx:xx\",\"last_status_store\":1523889831,\"modules\":[{\"_id\":\"02:00:00:2c:xx:xx\",\"type\":\"NAModule1\",\"last_message\":1523889829,\"last_seen\":1523889816,\"dashboard_data\":{\"Temperature\":24.3,\"temp_trend\":\"stable\",\"Humidity\":40,\"time_utc\":1523889765,\"date_max_temp\":1523885867,\"date_min_temp\":1523851467,\"min_temp\":21.4,\"max_temp\":24.7},\"data_type\":[\"Temperature\",\"Humidity\"],\"module_name\":\"Exterieur\",\"last_setup\":1518622000,\"battery_vp\":5924,\"battery_percent\":97,\"rf_status\":9,\"firmware\":46}],\"place\":{\"altitude\":-55.681362,\"city\":\"Puteaux\",\"country\":\"FR\",\"timezone\":\"Europe\\/Paris\",\"location\":[2.2389,48.8834]},\"station_name\":\"Test\",\"type\":\"NAMain\",\"dashboard_data\":{\"Temperature\":23.7,\"temp_trend\":\"down\",\"Humidity\":42,\"AbsolutePressure\":1033.2,\"Pressure\":1026.4,\"pressure_trend\":\"up\",\"Noise\":53,\"CO2\":1099,\"time_utc\":1523889815,\"date_max_temp\":1523885587,\"date_min_temp\":1523850877,\"min_temp\":21,\"max_temp\":24.4},\"data_type\":[\"Temperature\",\"CO2\",\"Humidity\",\"Noise\",\"Pressure\"],\"co2_calibrating\":false,\"date_setup\":1518621999,\"last_setup\":1518621999,\"module_name\":\"Indoor\",\"firmware\":134,\"last_upgrade\":1518622001,\"wifi_statuswifi_status\":47,\"friend_users\":[\"5a856f288af105312c8xxxxx\"]}],\"user\":{\"mail\":\"example@domain.com\",\"administrative\":{\"lang\":\"fr-FR\",\"reg_locale\":\"en-FR\",\"country\":\"FR\",\"unit\":0,\"windunit\":0,\"pressureunit\":0,\"feel_like_algo\":0}}},\"status\":\"ok\",\"time_exec\":0.075635195,\"time_server\":1523890283}");

            var sut = new Netatmo.WeatherClient("https://api.netatmo.com/", credentialManagerMock.Object);
            var result = await sut.GetStationsData();

            httpTest
                .ShouldHaveCalled("https://api.netatmo.com/api/getstationsdata")
                .WithVerb(HttpMethod.Post)
                .WithOAuthBearerToken(accessToken)
                .WithContentType("application/json").WithRequestJson(new GetStationsDataRequest())
                .Times(1);

            result.Body.Should().BeOfType<GetStationsDataBody>();
            result.Body.Devices[0].DashboardData.Noise.Should().Be(53);
            result.Body.Devices[0].WifiStrength.Should().Be(WifiStrengthEnum.Good);
            result.Body.Devices[0].Modules[0].GetDashboardData<OutdoorDashBoardData>().HumidityPercent.Should().Be(40);
            result.Body.Devices[0].Modules[0]
                .Invoking(y => y.GetDashboardData<WindGaugeDashBoardData>())
                .Should().Throw<ArgumentException>()
                .WithMessage("OutdoorDashBoardData should be expected");
            result.Body.Devices[0].Modules[0].RfStrength.Should().Be(RfStrengthEnum.FullSignal);
            result.Body.Devices[0].Modules[0].BatteryStatus.Should().Be(BatteryLevelEnum.Full);
            result.Body.Devices[0].Place.Timezone.Should().Be(DateTimeZoneProviders.Tzdb["Europe/Paris"]);
        }
    }
}