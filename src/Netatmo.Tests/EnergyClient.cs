using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Flurl.Http.Testing;
using Moq;
using Netatmo.Enums;
using Netatmo.Models.Client;
using Netatmo.Models.Client.Energy;
using Netatmo.Models.Client.Energy.RoomMeasure;
using Netatmo.Tests.ClassData.Energy;
using NodaTime;
using Xunit;

namespace Netatmo.Tests
{
    public class EnergyClient : IDisposable
    {
        private readonly string accessToken;

        private readonly HttpTest httpTest;
        private Mock<ICredentialManager> credentialManagerMock;

        public EnergyClient()
        {
            httpTest = new HttpTest();
            httpTest.Configure(Configuration.ConfigureRequest);

            accessToken = "Super-Access-Token";
            credentialManagerMock = new Mock<ICredentialManager>();
            credentialManagerMock.Setup(x => x.AccessToken).Returns(accessToken);
        }

        public void Dispose()
        {
            httpTest.Dispose();
        }

        [Fact]
        public async Task CreateHomeSchedule_Should_Return_Expected_Result()
        {
            var parameters = new CreateHomeScheduleRequest("5a327cbdb05a2133678b5d3e", 14, 16, "Cat schedule");
            httpTest.RespondWithJson(new CreateHomeScheduleResponse
            {
                ScheduleId = "5a819e6113475d09c28b497a", Status = "ok", TimeExec = 0.036107063293457, TimeServer = Instant.FromUnixTimeSeconds(1518023467)
            });

            var sut = new Netatmo.EnergyClient("https://api.netatmo.com/", credentialManagerMock.Object);
            await sut.CreateHomeSchedule(parameters);

            httpTest
                .ShouldHaveCalled("https://api.netatmo.com/api/createnewhomeschedule")
                .WithVerb(HttpMethod.Post)
                .WithOAuthBearerToken(accessToken)
                .WithContentType("application/json")
                .WithRequestJson(
                    new CreateHomeScheduleRequest(
                        parameters.HomeId,
                        parameters.HgTemp,
                        parameters.AwayTemp,
                        parameters.Name,
                        new Timetable[0],
                        new Zone[0]))
                .Times(1);
        }

        [Fact]
        public async Task DeleteHomeSchedule_Should_Return_Expected_Result()
        {
            var homeId = "5a327cbdb05a2133678b5d3e";
            var scheduleId = "5a327cbdb05a2133678b5d3f";
            httpTest.RespondWithJson(new DataResponse { Status = "ok", TimeExec = 0.036107063293457, TimeServer = Instant.FromUnixTimeSeconds(1518023467) });

            var sut = new Netatmo.EnergyClient("https://api.netatmo.com/", credentialManagerMock.Object);
            await sut.DeleteHomeSchedule(homeId, scheduleId);

            httpTest
                .ShouldHaveCalled("https://api.netatmo.com/api/deletehomeschedule")
                .WithVerb(HttpMethod.Post)
                .WithOAuthBearerToken(accessToken)
                .WithContentType("application/json").WithRequestJson(new DeleteHomeScheduleRequest { HomeId = homeId, ScheduleId = scheduleId })
                .Times(1);
        }

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
            httpTest.RespondWith(
                "{\"status\":\"ok\",\"time_server\":1518023129,\"body\":{\"home\":{\"modules\":[{\"id\":\"70:ee:50:23:d7:a8\",\"type\":\"NAPlug\",\"rf_strength\":104,\"wifi_strength\":38},{\"id\":\"04:00:00:23:f2:10\",\"reachable\":true,\"type\":\"NATherm1\",\"firmware_revision\":65,\"rf_strength\":26,\"battery_level\":4478,\"boiler_valve_comfort_boost\":false,\"boiler_status\":false,\"anticipating\":false,\"bridge\":\"70:ee:50:23:d7:a8\",\"battery_state\":\"full\"},{\"id\":\"09:00:00:00:0b:bd\",\"reachable\":true,\"type\":\"NRV\",\"firmware_revision\":51,\"rf_strength\":44,\"battery_level\":2982,\"bridge\":\"70:ee:50:23:d7:a8\",\"battery_state\":\"high\"}],\"rooms\":[{\"id\":\"2255031728\",\"reachable\":true,\"therm_measured_temperature\":25.3,\"therm_setpoint_temperature\":16,\"therm_setpoint_mode\":\"schedule\",\"therm_setpoint_start_time\":1517986800,\"therm_setpoint_end_time\":0},{\"id\":\"2539094912\",\"reachable\":true,\"therm_measured_temperature\":24,\"heating_power_request\":0,\"therm_setpoint_temperature\":16,\"therm_setpoint_mode\":\"schedule\",\"therm_setpoint_start_time\":1517986800,\"therm_setpoint_end_time\":0}],\"id\":\"5a327cbdb05a2133678b5d3e\"}}}");

            var sut = new Netatmo.EnergyClient("https://api.netatmo.com/", credentialManagerMock.Object);
            var result = await sut.GetHomeStatus(homeId);

            httpTest
                .ShouldHaveCalled("https://api.netatmo.com/api/homestatus")
                .WithVerb(HttpMethod.Post)
                .WithOAuthBearerToken(accessToken)
                .WithContentType("application/json").WithRequestJson(new GetHomeStatusRequest { HomeId = homeId })
                .Times(1);

            result.Body.Should().BeOfType<GetHomeStatusBody>();
            result.Body.Home.Modules[0].WifiStatus.Should().Be(WifiStrengthEnum.Good);
            result.Body.Home.Modules[0].RfStatus.Should().Be(RfStrengthEnum.Low);

            result.Body.Home.Modules[1].WifiStatus.Should().Be(WifiStrengthEnum.Undefined);
            result.Body.Home.Modules[1].BatteryStatus.Should().Be(BatteryLevelEnum.Full);

            result.Body.Home.Modules[2].BatteryStatus.Should().Be(BatteryLevelEnum.High);
        }

        [Fact]
        public async Task GetRoomMeasure_Should_Return_TemperatureSteps()
        {
            var parameters = new GetRoomMeasureParameters
            {
                HomeId = "5a327cbdb05a2133678b5d3e", RoomId = "2255031728", Scale = Scale.Max, Type = ThermostatMeasurementType.Temperature
            };

            httpTest.RespondWith(
                "{\"body\":[{\"beg_time\":1513259100,\"step_time\":1800,\"value\":[[27.9],[27.1],[26.2],[25.4],[25.8],[26.2],[26.7],[26.9],[27],[27.1],[27.2],[27],[26.8],[26.6],[26.5],[26.3],[26.3],[26.3],[26.2],[26.2],[26.2],[26.1],[26.1],[26.1],[26.1],[26.1],[26.1],[26.1],[26.1],[26],[26],[26.1],[26],[26],[26],[26],[26],[25.9],[24.8],[24],[23.6],[23.5],[23.2],[23],[22.9],[22.7],[22.5],[22.4],[22.4],[22.6],[22.8],[23.1],[23.2],[23.4],[23.4],[23.3],[23.1],[22.9],[22.5],[22.2],[21.9],[21.6],[21.6],[21.4],[21.2],[21.1],[21],[20.9],[20.8],[20.7],[20.7],[20.6],[20.6],[20.6],[20.5],[20.5],[20.5],[20.4],[20.4],[20.4],[20.4],[20.4],[20.3],[20.3],[20.2],[20.2],[20.2],[20.2],[20.2],[20.2],[20.2],[20.2],[20.2],[20.2],[20.2],[20.3],[20.3],[20.3],[20.3],[20.2],[20.2],[20.1],[20],[20],[19.9],[19.8],[19.8],[19.8],[19.7],[19.7],[19.7],[19.6],[19.9],[20.2],[20.6],[20.6],[20.7],[20.6],[20.7],[20.7],[20.7],[20.8],[20.7],[20.8],[20.7],[20.8],[20.5],[20.2],[20],[19.9],[19.7],[19.6],[19.5],[19.5],[19.4],[19.4],[19.4],[19.5],[19.5],[19.6],[19.6],[19.6],[19.7],[19.7],[19.7],[19.7],[19.7],[19.7],[19.6],[19.6],[19.5],[19.4],[19.4],[19.3],[19.3],[19.3],[19.3],[19.2],[19.2],[19.3],[20],[20.6],[20.8],[20.9],[20.8],[20.9],[21],[20.8],[20.9],[20.9],[20.7],[20.8],[20.8],[20.7],[20.4],[20.2],[20],[19.9],[19.7],[19.6],[19.6],[19.5],[19.7],[20.3],[20.6],[21],[21.3],[21.7],[21.9],[21.7],[21.6],[21.6],[21.5],[21.6],[21.9],[22.1],[22],[22],[22.1],[22.2],[22.3],[22.2],[21.9],[21.5],[21.1],[20.7],[20.5],[20.3],[20.4],[20.6],[20.7],[20.7],[20.8],[20.7],[20.8],[20.7],[20.8],[20.7],[20.8],[20.7],[20.8],[20.8],[20.5],[20.2],[20],[19.9],[19.7],[19.7],[19.6],[19.5],[19.7],[20.2],[20.6],[21],[21.4],[21.7],[21.7],[21.5],[21.2],[21],[21],[21.2],[21.5],[21.8],[22.2],[22.2],[22.3],[22.6],[23.4],[23.8],[24.2],[24.4],[24.4],[24.4],[24.5],[24.5],[24.5],[24.6],[24.6],[24.6],[24.6],[24.6],[24.7],[24.7],[24.7],[24.7],[24.8],[24.8],[24.8],[24.9],[24.9],[24.9],[25],[25],[25.1],[25.1],[25.1],[25.2],[25.2],[25.3],[25.6],[25.9],[26.1],[26.4],[26.5],[26.4],[26.3],[26.2],[26.3],[26.4],[26.5],[26.7],[26.8],[26.7],[26.9],[27],[27],[26.7],[26.6],[26.5],[26.3],[26.2],[26.1],[26],[26],[26],[26],[25.9],[25.9],[25.9],[25.9],[25.9],[25.9],[25.9],[25.9],[25.9],[25.9],[25.9],[25.9],[26],[26],[26],[26],[26],[26.1],[26.1],[25.2],[23.9],[23.2],[24.1],[25.1],[25.9],[26.3],[26.6],[26.6],[26.2],[25.7],[25],[24.4],[25.1],[25.7],[26.2],[26.3],[26.1],[25.9],[25.2],[24],[22.9],[21.9],[21],[20.2],[19.6],[19.3],[19.1],[19],[18.9],[18.8],[18.7],[18.6],[18.5],[18.4],[18.3],[18.2],[18.1],[18],[17.9],[17.8],[17.6],[17.4],[17.3],[17.1],[17],[17],[17.3],[18.2],[20.2],[21.9],[23.2],[24.1],[24.8],[25.2],[24.8],[23.9],[24.3],[25],[25.6],[26],[26.2],[26.3],[26.4],[26.4],[26.5],[26.5],[26.4],[26.3],[26.2],[26.1],[26],[25.9],[25.9],[25.9],[25.8],[25.8],[25.8],[25.8],[25.8],[25.8],[25.8],[25.8],[25.9],[25.9],[25.9],[25.9],[25.9],[25.9],[25.9],[25.9],[26],[26],[26],[26],[26],[26],[26],[26.1],[26.1],[26.1],[26.2],[26.2],[26.2],[26.2],[26.2],[26.3],[26.3],[26.3],[26.3],[26.2],[26.2],[26.2],[26.2],[26.2],[26.1],[26.1],[26.1],[26.1],[26.1],[26.1],[26.1],[26.1],[26.1],[26.1],[26.1],[26.1],[26.1],[26.1],[26.1],[26.1],[26.1],[26.1],[26.1],[26.1],[26.1],[26.1],[26.1],[26.2],[26.2],[26.2],[26.2],[26.2],[26.2],[26.2],[26.2],[26.3],[26.3],[26.3],[26.3],[26.3],[26.3],[26.3],[26.3],[26.3],[26.3],[26.3],[26.3],[26.3],[26.3],[26.2],[26.2],[26.2],[26.2],[26.2],[26.2],[26.2],[26.2],[26.2],[26.2],[26.2],[26.2],[26.2],[26.2],[26.2],[26.2],[26.2],[26.2],[26.2],[26.2],[26.1],[26.1],[26.1],[26.1],[26.1],[26.1],[26.2],[26.2],[26.2],[26.2],[26.2],[26.2],[26.2],[26.2],[26.2],[26.2],[26.3],[26.3],[26.4],[26.4],[26.5],[26.4],[26.4],[26.3],[26.3],[26.3],[26.3],[26.2],[26.2],[26.2],[26.2],[26.2],[26.1],[26.1],[26.1],[26.1],[26.1],[26.1],[26.1],[26.1],[26],[26],[25.9],[25.9],[25.8],[25.8],[25.8],[25.7],[25.7],[25.7],[25.7],[25.7],[25.7],[25.7],[25.8],[25.9],[26],[26],[24.8],[23.8],[24.4],[24.9],[25.3],[25.8],[26.1],[26.4],[26.6],[26.7],[26.7],[26.8],[26.9],[26.9],[27.1],[27.2],[27.3],[27.3],[27.3],[27.3],[27.2],[27.1],[27],[26.9],[26.8],[26.7],[26.6],[26.5],[26.5],[26.4],[26.4],[26.3],[26.2],[26.1],[26.1],[26],[26],[25.9],[25.9],[25.9],[25.8],[25.8],[25.8],[25.9],[26],[26.1],[26.2],[26.2],[26.3],[25.1],[24.9],[25.2],[25.6],[26],[26.4],[26.6],[26.7],[26.7],[26.7],[26.6],[26.6],[26.7],[26.7],[26.9],[26.6],[26.5],[26.6],[26.7],[26.7],[26.5],[26.5],[26.4],[26.4],[26.4],[26.4],[26.4],[26.3],[26.3],[26.2],[26.2],[26.1],[26.1],[26],[26],[25.9],[25.9],[25.9],[25.8],[25.8],[25.8],[25.8],[25.8],[25.8],[25.9],[25.9],[25.9],[25.9],[26],[26.1],[25.9],[24.8],[25.2],[25.7],[26.3],[26.6],[26.7],[26.8],[26.8],[26.7],[26.9],[27],[27.1],[27.1],[26.8],[26.3],[25.4],[24.6],[23.8],[23.2],[22.6],[22.2],[21.9],[21.6],[21.5],[21.4],[21.3],[21.2],[21.1],[21],[21],[20.9],[20.8],[20.8],[20.7],[20.7],[20.6],[20.6],[20.6],[20.5],[20.5],[20.4],[20.4],[20.3],[20.2],[20.2],[20.2],[20.3],[20.5],[20.7],[20.9],[21.1],[21.2],[21.3],[21.3],[21.3],[21.3],[21.3],[21.4],[21.5],[21.5],[21.6],[21.6],[21.6],[21.6],[21.6],[21.4],[21],[20.8],[20.6],[20.4],[20.3],[20.2],[20.4],[20.5],[20.5],[20.5],[20.5],[20.5],[20.5],[20.6],[20.5],[20.6],[20.5],[20.6],[20.5],[20.6],[20.4],[20.3],[20.3],[20.2],[20.2],[20.2],[20.2],[20.2],[20.2],[20.2],[20.3],[20.3],[20.3],[20.4],[20.4],[20.5],[20.5],[20.4],[20.4],[20.4],[20.4],[20.4],[20.4],[20.3],[20.3],[20.3],[20.3],[20.3],[20.2],[20.2],[20.2],[20.2],[20.2],[20.2],[20.5],[20.7],[20.7],[20.8],[20.7],[20.7],[20.7],[20.8],[20.8],[20.7],[20.8],[20.7],[20.8],[20.7],[20.5],[20.4],[20.3],[20.2],[20.2],[20.1],[20.1],[20.1],[20.1],[20.1],[20.1],[20.1],[20.1],[20.1],[20.2],[20.2],[20.3],[20.4],[20.3],[20.3],[20.3],[20.2],[20.2],[20.1],[20],[20],[19.9],[19.9],[19.9],[19.9],[19.8],[19.8],[19.8],[19.8],[20],[20.3],[20.7],[20.7],[20.8],[20.7],[20.8],[20.7],[20.8],[20.7],[20.8],[20.7],[20.8],[20.7],[20.4],[20.1],[20],[19.9],[19.7],[19.7],[19.5],[19.5],[19.5],[19.4],[19.4],[19.4],[19.4],[19.4],[19.4],[19.4],[19.4],[19.5],[19.7],[19.7],[19.6],[19.6],[19.5],[19.5],[19.5],[19.4],[19.4],[19.4],[19.3],[19.3],[19.3],[19.3],[19.3],[19.4],[20.2],[20.7],[20.9],[20.7],[20.8],[20.8],[20.7],[20.8],[20.7],[20.7],[20.8],[20.7],[20.8],[20.7],[20.4],[20.1],[19.8],[19.7],[19.6],[19.5],[19.4],[19.4],[19.5],[19.9],[20.5],[20.8],[21],[21.3],[21.3],[21.1],[20.9],[20.9],[21.2],[21.5],[21.8],[22.1],[22.2],[22.3],[22.3],[22.4],[22.4],[22.1],[21.7],[21.3],[21],[20.7],[20.5],[20.4],[20.6],[20.7],[20.7],[20.9],[20.8],[20.8],[20.8],[20.8],[20.9],[20.8],[20.8],[20.8],[20.8],[20.9],[20.6],[20.4],[20.3],[20.2],[20.1],[20.1],[20],[19.9],[20.2],[20.7],[21.2],[21.7],[21.9],[22],[21.9],[21.6],[21.5],[21.6],[21.8],[22]]},{\"beg_time\":1514992500,\"step_time\":1800,\"value\":[[22.8],[22.9],[22.8],[22.7],[22.8],[22.8],[22.7],[22.3],[21.8],[21.4],[21.1],[20.8],[20.6],[20.6],[20.8],[20.8],[20.9],[20.8],[20.9],[20.9],[20.9],[20.9],[20.9],[20.9],[20.9],[20.9],[20.9],[20.7],[20.6],[20.4],[20.4],[20.3],[20.3],[20.3],[20.3],[20.6],[21.2],[21.4],[21.6],[21.8],[22.1],[22.2],[22],[21.9],[21.9],[22],[22.3],[22.5],[22.6],[22.7],[22.8],[22.8],[22.9],[22.6],[22.3],[21.7],[21.4],[21.1],[20.9],[20.7],[20.5],[20.7]]}],\"status\":\"ok\",\"time_exec\":0.79246497154236,\"time_server\":1518023284}");

            var sut = new Netatmo.EnergyClient("https://api.netatmo.com/", credentialManagerMock.Object);
            var result = await sut.GetRoomMeasure<TemperatureStep>(parameters);

            httpTest
                .ShouldHaveCalled("https://api.netatmo.com/api/getroommeasure")
                .WithVerb(HttpMethod.Post)
                .WithOAuthBearerToken(accessToken)
                .WithContentType("application/json").WithRequestJson(new GetRoomMeasureRequest
                {
                    HomeId = parameters.HomeId, RoomId = parameters.RoomId, Scale = parameters.Scale.Value, Type = parameters.Type.Value
                })
                .Times(1);

            result.Body[0].BeginAt.Should().Be(Instant.FromDateTimeUtc(DateTime.SpecifyKind(new DateTime(2017, 12, 14, 13, 45, 0), DateTimeKind.Utc)));
            result.Body.Length.Should().Be(2);
            result.Body[0].Values[0][0].Should().Be(27.9);
            result.Body[0].Values[1][0].Should().Be(27.1);
        }

        [Fact]
        public void GetRoomMeasure_With_Bad_Type_Should_Throw_ArgumentException()
        {
            var sut = new Netatmo.EnergyClient("https://api.netatmo.com/", credentialManagerMock.Object);
            Func<Task> actTemperatureStep = async () =>
            {
                await sut.GetRoomMeasure<DateTemperatureStep>(new GetRoomMeasureParameters
                {
                    HomeId = "5a327cbdb05a2133678b5d3e", RoomId = "2255031728", Scale = Scale.Max, Type = ThermostatMeasurementType.Temperature
                });
            };

            actTemperatureStep
                .Should().Throw<ArgumentException>()
                .WithMessage("TemperatureStep should be used with a temperature measurement");

            Func<Task> actDateTemperatureStep = async () =>
            {
                await sut.GetRoomMeasure<TemperatureStep>(new GetRoomMeasureParameters
                {
                    HomeId = "5a327cbdb05a2133678b5d3e", RoomId = "2255031728", Scale = Scale.OneMonth, Type = ThermostatMeasurementType.DateMinTemp
                });
            };

            actDateTemperatureStep
                .Should().Throw<ArgumentException>()
                .WithMessage("DateTemperatureStep should be used with a date of temperature measurement");
        }

        [Fact]
        public async Task RenameHomeSchedule_Should_Return_Expected_Result()
        {
            var homeId = "5a327cbdb05a2133678b5d3e";
            var scheduleId = "5a327cbdb05a2133678b5d3f";
            var name = "Cat schedule";
            httpTest.RespondWithJson(new DataResponse { Status = "ok", TimeExec = 0.036107063293457, TimeServer = Instant.FromUnixTimeSeconds(1518023467) });

            var sut = new Netatmo.EnergyClient("https://api.netatmo.com/", credentialManagerMock.Object);
            await sut.RenameHomeSchedule(homeId, scheduleId, name);

            httpTest
                .ShouldHaveCalled("https://api.netatmo.com/api/renamehomeschedule")
                .WithVerb(HttpMethod.Post)
                .WithOAuthBearerToken(accessToken)
                .WithContentType("application/json").WithRequestJson(new RenameHomeScheduleRequest { HomeId = homeId, ScheduleId = scheduleId, Name = name })
                .Times(1);
        }

        [Fact]
        public async Task SetRoomThermPoint_Should_Return_Expected_Result()
        {
            var homeId = "5a327cbdb05a2133678b5d3e";
            var roomId = "2255031728";
            var mode = "schedule";
            httpTest.RespondWithJson(new DataResponse { Status = "ok", TimeExec = 0.036107063293457, TimeServer = Instant.FromUnixTimeSeconds(1518023467) });

            var sut = new Netatmo.EnergyClient("https://api.netatmo.com/", credentialManagerMock.Object);
            await sut.SetRoomThermPoint(homeId, roomId, mode);

            httpTest
                .ShouldHaveCalled("https://api.netatmo.com/api/setroomthermpoint")
                .WithVerb(HttpMethod.Post)
                .WithOAuthBearerToken(accessToken)
                .WithContentType("application/json").WithRequestJson(new SetRoomThermpointRequest { HomeId = homeId, RoomId = roomId, Mode = mode })
                .Times(1);
        }

        [Fact]
        public async Task SetThermMode_Should_Return_Expected_Result()
        {
            var homeId = "5a327cbdb05a2133678b5d3e";
            var mode = "schedule";
            httpTest.RespondWithJson(new DataResponse { Status = "ok", TimeExec = 0.036107063293457, TimeServer = Instant.FromUnixTimeSeconds(1518023467) });

            var sut = new Netatmo.EnergyClient("https://api.netatmo.com/", credentialManagerMock.Object);
            await sut.SetThermMode(homeId, mode);

            httpTest
                .ShouldHaveCalled("https://api.netatmo.com/api/setthermmode")
                .WithVerb(HttpMethod.Post)
                .WithOAuthBearerToken(accessToken)
                .WithContentType("application/json").WithRequestJson(new SetThermModeRequest { HomeId = homeId, Mode = mode })
                .Times(1);
        }

        [Fact]
        public async Task SwitchHomeSchedule_Should_Return_Expected_Result()
        {
            var homeId = "5a327cbdb05a2133678b5d3e";
            var scheduleId = "5a327cbdb05a2133678b5d3f";
            httpTest.RespondWithJson(new DataResponse { Status = "ok", TimeExec = 0.036107063293457, TimeServer = Instant.FromUnixTimeSeconds(1518023467) });

            var sut = new Netatmo.EnergyClient("https://api.netatmo.com/", credentialManagerMock.Object);
            await sut.SwitchHomeSchedule(homeId, scheduleId);

            httpTest
                .ShouldHaveCalled("https://api.netatmo.com/api/switchhomeschedule")
                .WithVerb(HttpMethod.Post)
                .WithOAuthBearerToken(accessToken)
                .WithContentType("application/json").WithRequestJson(new SwitchHomeScheduleRequest { HomeId = homeId, ScheduleId = scheduleId })
                .Times(1);
        }

        [Fact]
        public async Task SyncHomeSchedule_Should_Return_Expected_Result()
        {
            var parameters = new SyncHomeScheduleRequest("5a327cbdb05a2133678b5d3e", "5a327cbdb05a2133678b5d3f", 14, 16);
            httpTest.RespondWithJson(new DataResponse { Status = "ok", TimeExec = 0.036107063293457, TimeServer = Instant.FromUnixTimeSeconds(1518023467) });

            var sut = new Netatmo.EnergyClient("https://api.netatmo.com/", credentialManagerMock.Object);
            await sut.SyncHomeSchedule(parameters);

            httpTest
                .ShouldHaveCalled("https://api.netatmo.com/api/synchomeschedule")
                .WithVerb(HttpMethod.Post)
                .WithOAuthBearerToken(accessToken)
                .WithContentType("application/json")
                .WithRequestJson(
                    new SyncHomeScheduleRequest(
                        parameters.HomeId,
                        parameters.ScheduleId,
                        parameters.HgTemp,
                        parameters.AwayTemp,
                        new Timetable[0],
                        new Zone[0]))
                .Times(1);
        }

        [Theory]
        [ClassData(typeof(GetRoomMeasureArgumentExceptionData))]
        public void GetRoomMeasure_Should_Throw_ArgumentException(GetRoomMeasureParameters parameters, string exceptionMessage)
        {
            var sut = new Netatmo.EnergyClient("https://api.netatmo.com/", credentialManagerMock.Object);
            Func<Task> act = async () => { await sut.GetRoomMeasure<TemperatureStep>(parameters); };

            act
                .Should().Throw<ArgumentException>()
                .WithMessage(exceptionMessage);
        }
    }
}