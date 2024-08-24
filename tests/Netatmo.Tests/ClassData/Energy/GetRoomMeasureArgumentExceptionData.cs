using System.Collections;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Netatmo.Models.Client.Energy;
using NodaTime;
using NodaTime.Testing;

namespace Netatmo.Tests.ClassData.Energy;

public class GetRoomMeasureArgumentExceptionData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        var fixture = new Fixture().Customize(new AutoNSubstituteCustomization { ConfigureMembers = true });
        fixture.Register<IClock>(() => new FakeClock(Instant.FromDateTimeOffset(fixture.Create<DateTimeOffset>())));
        fixture.Customize<CredentialManager>(
            c => c.FromFactory<string, string, IClock>((clientId, clientSecret, clock) => new CredentialManager("https://api.netatmo.local", clientId, clientSecret, clock))
                .OmitAutoProperties());
        fixture.Customize<EnergyClient>(c => c.FromFactory<ICredentialManager>(credentialManager => new EnergyClient("https://api.netatmo.local", credentialManager)));

        yield return [new GetRoomMeasureParameters(), "Home Id shouldn't be null", fixture.Create<EnergyClient>()];
        yield return [new GetRoomMeasureParameters { HomeId = "5a327cbdb05a2133678b5d3e" }, "Room Id shouldn't be null", fixture.Create<EnergyClient>()];
        yield return [new GetRoomMeasureParameters { HomeId = "5a327cbdb05a2133678b5d3e", RoomId = "2255031728" }, "Scale shouldn't be null", fixture.Create<EnergyClient>()];
        yield return
        [
            new GetRoomMeasureParameters { HomeId = "5a327cbdb05a2133678b5d3e", RoomId = "2255031728", Scale = Scale.Max }, "Type shouldn't be null", fixture.Create<EnergyClient>()
        ];
        yield return
        [
            new GetRoomMeasureParameters { HomeId = "5a327cbdb05a2133678b5d3e", RoomId = "2255031728", Scale = Scale.Max, Type = ThermostatMeasurementType.DateMinTemp },
            "Type shouldn't be allow for this scale",
            fixture.Create<EnergyClient>()
        ];
        yield return
        [
            new GetRoomMeasureParameters
            {
                HomeId = "5a327cbdb05a2133678b5d3e",
                RoomId = "2255031728",
                Scale = Scale.Max,
                Type = ThermostatMeasurementType.Temperature,
                Limit = 2000
            },
            "Limit should be between 0 and 1024",
            fixture.Create<EnergyClient>()
        ];
        yield return
        [
            new GetRoomMeasureParameters
            {
                HomeId = "5a327cbdb05a2133678b5d3e",
                RoomId = "2255031728",
                Scale = Scale.Max,
                Type = ThermostatMeasurementType.Temperature,
                Limit = -42
            },
            "Limit should be between 0 and 1024",
            fixture.Create<EnergyClient>()
        ];
        yield return
        [
            new GetRoomMeasureParameters
            {
                HomeId = "5a327cbdb05a2133678b5d3e",
                RoomId = "2255031728",
                Scale = Scale.Max,
                Type = ThermostatMeasurementType.Temperature,
                BeginAt = Instant.FromUtc(2019, 4, 30, 9, 42),
                EndAt = Instant.FromUtc(2017, 4, 30, 13, 37)
            },
            "BeginAt should be lower than EndAt",
            fixture.Create<EnergyClient>()
        ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}