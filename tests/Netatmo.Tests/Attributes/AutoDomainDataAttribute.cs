using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using NodaTime;
using NodaTime.Testing;

namespace Netatmo.Tests.Attributes;

public class AutoDomainDataAttribute()
    : AutoDataAttribute(
        () => new Fixture().Customize(new AutoNSubstituteCustomization { ConfigureMembers = true })
            .Customize(new ClockCustomization())
            .Customize(new CredentialManagerCustomization())
            .Customize(new ClientsCustomization()));

public class ClockCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Register(() => Instant.FromDateTimeOffset(fixture.Create<DateTimeOffset>()));
        fixture.Register<IClock>(() => new FakeClock(fixture.Create<Instant>()));
    }
}

public class CredentialManagerCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<CredentialManager>(
            c => c.FromFactory<string, string, IClock>((clientId, clientSecret, clock) => new CredentialManager("https://api.netatmo.local", clientId, clientSecret, clock))
                .OmitAutoProperties());
    }
}

public class ClientsCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<AirClient>(c => c.FromFactory<ICredentialManager>(credentialManager => new AirClient("https://api.netatmo.local", credentialManager)));
        fixture.Customize<WeatherClient>(c => c.FromFactory<ICredentialManager>(credentialManager => new WeatherClient("https://api.netatmo.local", credentialManager)));
        fixture.Customize<EnergyClient>(c => c.FromFactory<ICredentialManager>(credentialManager => new EnergyClient("https://api.netatmo.local", credentialManager)));
    }
}