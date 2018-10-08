using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Flurl.Http.Testing;
using Moq;
using Netatmo.Models.Client;
using Netatmo.Models.Client.Energy;
using Netatmo.Models.Client.Enums;
using NodaTime;
using Xunit;

namespace Netatmo.Tests
{
    public class EnergyClient : IDisposable
    {
        public EnergyClient()
        {
            httpTest = new HttpTest();

            accessToken = "Super-Access-Token";
            credentialManagerMock = new Mock<ICredentialManager>();
            credentialManagerMock.Setup(x => x.AccessToken).Returns(accessToken);
        }

        public void Dispose()
        {
            httpTest.Dispose();
            credentialManagerMock = null;
        }

        private readonly HttpTest httpTest;
        private readonly string accessToken;
        private Mock<ICredentialManager> credentialManagerMock;

        [Fact]
        public async Task GetHomesData_Should_Return_DataResponse_With_HomesData()
        {
            httpTest.RespondWith(
                "{\"body\":{\"homes\":[{\"id\":\"5a327cbdb05a2133678b5d3e\",\"name\":\"test\",\"altitude\":88,\"coordinates\":[2.2395809,48.829662],\"country\":\"FR\",\"timezone\":\"Europe\\/Paris\",\"rooms\":[{\"id\":\"2255031728\",\"name\":\"Salon\",\"type\":\"livingroom\",\"module_ids\":[\"04:00:00:23:f2:10\"],\"measure_offset_NAPlug_temperature\":0,\"measure_offset_NAPlug_estimated_temperature\":0},{\"id\":\"2539094912\",\"name\":\"Chambre\",\"type\":\"bedroom\",\"module_ids\":[\"09:00:00:00:0b:bd\"],\"measure_offset_NAPlug_temperature\":0,\"measure_offset_NAPlug_estimated_temperature\":0}],\"modules\":[{\"id\":\"70:ee:50:23:d7:a8\",\"type\":\"NAPlug\",\"name\":\"Relais\",\"setup_date\":1513259804,\"modules_bridged\":[\"04:00:00:23:f2:10\",\"09:00:00:00:0b:bd\"]},{\"id\":\"04:00:00:23:f2:10\",\"type\":\"NATherm1\",\"name\":\"Thermostat\",\"setup_date\":1513259817,\"room_id\":\"2255031728\",\"bridge\":\"70:ee:50:23:d7:a8\"},{\"id\":\"09:00:00:00:0b:bd\",\"type\":\"NRV\",\"name\":\"Vanne Salon\",\"setup_date\":1513260804,\"room_id\":\"2539094912\",\"bridge\":\"70:ee:50:23:d7:a8\"}],\"schedules\":[{\"away_temp\":12,\"default\":true,\"hg_temp\":7,\"timetable\":[{\"m_offset\":0,\"zone_id\":1},{\"m_offset\":420,\"zone_id\":3},{\"m_offset\":450,\"zone_id\":0},{\"m_offset\":480,\"zone_id\":4},{\"m_offset\":1140,\"zone_id\":0},{\"m_offset\":1290,\"zone_id\":3},{\"m_offset\":1320,\"zone_id\":1},{\"m_offset\":1860,\"zone_id\":3},{\"m_offset\":1890,\"zone_id\":0},{\"m_offset\":1920,\"zone_id\":4},{\"m_offset\":2580,\"zone_id\":0},{\"m_offset\":2730,\"zone_id\":3},{\"m_offset\":2760,\"zone_id\":1},{\"m_offset\":3300,\"zone_id\":3},{\"m_offset\":3330,\"zone_id\":0},{\"m_offset\":3360,\"zone_id\":4},{\"m_offset\":4020,\"zone_id\":0},{\"m_offset\":4170,\"zone_id\":3},{\"m_offset\":4200,\"zone_id\":1},{\"m_offset\":4740,\"zone_id\":3},{\"m_offset\":4770,\"zone_id\":0},{\"m_offset\":4800,\"zone_id\":4},{\"m_offset\":5460,\"zone_id\":0},{\"m_offset\":5610,\"zone_id\":3},{\"m_offset\":5640,\"zone_id\":1},{\"m_offset\":6180,\"zone_id\":3},{\"m_offset\":6210,\"zone_id\":0},{\"m_offset\":6240,\"zone_id\":4},{\"m_offset\":6900,\"zone_id\":0},{\"m_offset\":7050,\"zone_id\":3},{\"m_offset\":7080,\"zone_id\":1},{\"m_offset\":7620,\"zone_id\":3},{\"m_offset\":7650,\"zone_id\":0},{\"m_offset\":8490,\"zone_id\":3},{\"m_offset\":8520,\"zone_id\":1},{\"m_offset\":9060,\"zone_id\":3},{\"m_offset\":9090,\"zone_id\":0},{\"m_offset\":9930,\"zone_id\":3},{\"m_offset\":9960,\"zone_id\":1}],\"zones\":[{\"type\":0,\"id\":0,\"rooms\":[{\"id\":\"2255031728\",\"therm_setpoint_temperature\":19},{\"room_id\":\"2539094912\",\"therm_setpoint_temperature\":17}]},{\"type\":1,\"id\":1,\"rooms\":[{\"id\":\"2255031728\",\"therm_setpoint_temperature\":16},{\"id\":\"2539094912\",\"therm_setpoint_temperature\":17}]},{\"type\":8,\"id\":3,\"rooms\":[{\"id\":\"2255031728\",\"therm_setpoint_temperature\":19},{\"id\":\"2539094912\",\"therm_setpoint_temperature\":17}]},{\"type\":5,\"id\":4,\"rooms\":[{\"id\":\"2255031728\",\"therm_setpoint_temperature\":16},{\"id\":\"2539094912\",\"therm_setpoint_temperature\":16}]}],\"id\":\"5a327cbdb05a2133678b5d3f\",\"selected\":true,\"type\":\"therm\"}],\"therm_setpoint_default_duration\":180,\"therm_mode\":\"schedule\"}],\"user\":{\"email\":\"example@domain.com\",\"language\":\"fr-FR\",\"locale\":\"en-FR\",\"feel_like_algorithm\":0,\"unit_pressure\":0,\"unit_system\":0,\"unit_wind\":0,\"id\":\"user_id\"}},\"status\":\"ok\",\"time_exec\":0.08151913,\"time_server\":1518022817}");

            var sut = new Netatmo.EnergyClient("https://api.netatmo.com/", credentialManagerMock.Object);
            var result = await sut.GetHomesData();

            httpTest
                .ShouldHaveCalled("https://api.netatmo.com/api/homesdata")
                .WithVerb(HttpMethod.Post)
                .WithOAuthBearerToken(accessToken)
                .WithContentType("application/json").WithRequestJson(new GetHomesDataRequest())
                .Times(1);

            result.Body.Should().BeOfType<GetHomesDataBody>();
            result.Body.Homes[0].Modules[0].ModulesBridged.Should().Equal("04:00:00:23:f2:10", "09:00:00:00:0b:bd");
        }

        [Fact]
        public async Task GetHomeStatus_Should_Return_DataResponse_With_HomeStatus()
        {
            var homeId = "5a327cbdb05a2133678b5d3e";
            httpTest.RespondWith("{\"status\":\"ok\",\"time_server\":1518023129,\"body\":{\"home\":{\"modules\":[{\"id\":\"70:ee:50:23:d7:a8\",\"type\":\"NAPlug\",\"rf_strength\":104,\"wifi_strength\":38},{\"id\":\"04:00:00:23:f2:10\",\"reachable\":true,\"type\":\"NATherm1\",\"firmware_revision\":65,\"rf_strength\":26,\"battery_level\":4478,\"boiler_valve_comfort_boost\":false,\"boiler_status\":false,\"anticipating\":false,\"bridge\":\"70:ee:50:23:d7:a8\",\"battery_state\":\"full\"},{\"id\":\"09:00:00:00:0b:bd\",\"reachable\":true,\"type\":\"NRV\",\"firmware_revision\":51,\"rf_strength\":44,\"battery_level\":2982,\"bridge\":\"70:ee:50:23:d7:a8\",\"battery_state\":\"high\"}],\"rooms\":[{\"id\":\"2255031728\",\"reachable\":true,\"therm_measured_temperature\":25.3,\"therm_setpoint_temperature\":16,\"therm_setpoint_mode\":\"schedule\",\"therm_setpoint_start_time\":1517986800,\"therm_setpoint_end_time\":0},{\"id\":\"2539094912\",\"reachable\":true,\"therm_measured_temperature\":24,\"heating_power_request\":0,\"therm_setpoint_temperature\":16,\"therm_setpoint_mode\":\"schedule\",\"therm_setpoint_start_time\":1517986800,\"therm_setpoint_end_time\":0}],\"id\":\"5a327cbdb05a2133678b5d3e\"}}}");
            
            var sut = new Netatmo.EnergyClient("https://api.netatmo.com/", credentialManagerMock.Object);
            var result = await sut.GetHomeStatus(homeId);

            httpTest
                .ShouldHaveCalled("https://api.netatmo.com/api/homestatus")
                .WithVerb(HttpMethod.Post)
                .WithOAuthBearerToken(accessToken)
                .WithContentType("application/json").WithRequestJson(new GetHomeStatusRequest()
                {
                    HomeId = homeId
                })
                .Times(1);
            
            result.Body.Should().BeOfType<GetHomeStatusBody>();
            result.Body.Home.Modules[0].WifiStatus.Should().Be(WifiStrengthEnum.Good);
            result.Body.Home.Modules[0].RfStatus.Should().Be(RfStrengthEnum.Low);

            result.Body.Home.Modules[1].WifiStatus.Should().Be(WifiStrengthEnum.Undefined);
            result.Body.Home.Modules[1].BatteryStatus.Should().Be(BatteryLevelEnum.Full);
            
            result.Body.Home.Modules[2].BatteryStatus.Should().Be(BatteryLevelEnum.High);
        }

        [Theory]
        [InlineData("ok", true)]
        [InlineData("error", false)]
        public async Task SetThermMode_Should_Return_Expected_Result(string status, bool expectedResult)
        {
            var homeId = "5a327cbdb05a2133678b5d3e";
            var mode = "schedule";
            httpTest.RespondWithJson(new DataResponse
            {
                Status = status, TimeExec = 0.036107063293457, 
                TimeServer = new LocalDateTime(1970, 1, 1, 0, 0, 0).PlusSeconds(1518023467)
            });
            
            var sut = new Netatmo.EnergyClient("https://api.netatmo.com/", credentialManagerMock.Object);
            var result = await sut.SetThermMode(homeId, mode);

            httpTest
                .ShouldHaveCalled("https://api.netatmo.com/api/setroomthermmode")
                .WithVerb(HttpMethod.Post)
                .WithOAuthBearerToken(accessToken)
                .WithContentType("application/json").WithRequestJson(new SetThermModeRequest{HomeId = homeId, Mode = mode})
                .Times(1);

            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("ok", true)]
        [InlineData("error", false)]
        public async Task SetRoomThermPoint_Should_Return_Expected_Result(string status, bool expectedResult)
        {
            var homeId = "5a327cbdb05a2133678b5d3e";
            var roomId = "2255031728";
            var mode = "schedule";
            httpTest.RespondWithJson(new DataResponse
            {
                Status = status, TimeExec = 0.036107063293457, 
                TimeServer = new LocalDateTime(1970, 1, 1, 0, 0, 0).PlusSeconds(1518023467)
            });
            
            var sut = new Netatmo.EnergyClient("https://api.netatmo.com/", credentialManagerMock.Object);
            var result = await sut.SetRoomThermPoint(homeId, roomId, mode);

            httpTest
                .ShouldHaveCalled("https://api.netatmo.com/api/setroomthermpoint")
                .WithVerb(HttpMethod.Post)
                .WithOAuthBearerToken(accessToken)
                .WithContentType("application/json").WithRequestJson(new SetRoomThermpointRequest{HomeId = homeId, RoomId = roomId, Mode = mode})
                .Times(1);

            result.Should().Be(expectedResult);
        }
    }
}