using System.Net.Mail;
using AutoFixture.Xunit2;
using Flurl.Http.Testing;
using Netatmo.Models;
using Netatmo.Tests.Attributes;
using NodaTime;
using NodaTime.Testing;

namespace Netatmo.Tests;

public class CredentialManagerTest : IDisposable
{
    private readonly HttpTest httpTest = new();

    public void Dispose()
    {
        httpTest.Dispose();
        GC.SuppressFinalize(this);
    }

    [Theory]
    [AutoDomainData]
    public async Task GenerateToken_Should_Acquire_Excepted_CredentialToken(
        TokenResponse expectedToken,
        IClock clock,
        string clientId,
        string clientSecret,
        MailAddress username,
        string password)
    {
        var scopes = new[]
        {
            Scope.CameraAccess,
            Scope.CameraRead,
            Scope.CameraWrite,
            Scope.HomecoachRead,
            Scope.PresenceAccess,
            Scope.PresenceRead,
            Scope.StationRead,
            Scope.StationWrite,
            Scope.ThermostatRead
        };

        httpTest.RespondWithJson(expectedToken);

        var sut = new CredentialManager("https://api.netatmo.local", clientId, clientSecret, clock);

        await sut.GenerateToken(username.ToString(), password, scopes);

        var token = sut.CredentialToken;

        httpTest.ShouldHaveCalled("https://api.netatmo.local/oauth2/token")
            .WithVerb(HttpMethod.Post)
            .WithContentType("application/x-www-form-urlencoded")
            .WithRequestUrlEncoded(
                new
                {
                    grant_type = "password",
                    client_id = clientId,
                    client_secret = clientSecret,
                    username = username.ToString(),
                    password,
                    scope = string.Join(' ', scopes.Select(scope => scope.Value))
                })
            .Times(1);

        token.Should().BeOfType<CredentialToken>();
        token.AccessToken.Should().Be(expectedToken.AccessToken);
        token.RefreshToken.Should().Be(expectedToken.RefreshToken);
        token.ExpiresAt.Should().Be(token.ReceivedAt.Plus(Duration.FromSeconds(expectedToken.ExpiresIn)));
    }

    [Theory]
    [AutoDomainData]
    public void ProvideOAuth2Token_Should_Provide_Token_From_Existing([Frozen] IClock clock, TokenResponse expectedToken, CredentialManager sut)
    {
        httpTest.RespondWithJson(expectedToken);

        sut.ProvideOAuth2Token(expectedToken.AccessToken);

        var token = sut.CredentialToken;

        token.Should().BeOfType<CredentialToken>().And.Be(new CredentialToken(20, expectedToken.AccessToken, null, clock.GetCurrentInstant()));
    }

    [Theory]
    [AutoDomainData]
    public async Task RefreshToken_Should_Refresh_Token(
        TokenResponse firstToken,
        TokenResponse expectedToken,
        string clientId,
        string clientSecret,
        MailAddress username,
        string password)
    {
        httpTest.RespondWithJson(firstToken);
        httpTest.RespondWithJson(expectedToken);

        var fakeClock = new FakeClock(SystemClock.Instance.GetCurrentInstant(), Duration.FromMinutes(-2));

        var sut = new CredentialManager("https://api.netatmo.local", clientId, clientSecret, fakeClock);

        await sut.GenerateToken(username.ToString(), password);
        var oldToken = sut.CredentialToken;
        await sut.RefreshToken();
        var refreshedToken = sut.CredentialToken;

        httpTest.ShouldHaveCalled("https://api.netatmo.local/oauth2/token")
            .WithVerb(HttpMethod.Post)
            .WithContentType("application/x-www-form-urlencoded")
            .WithRequestUrlEncoded(new { grant_type = "refresh_token", client_id = clientId, client_secret = clientSecret, refresh_token = oldToken.RefreshToken })
            .Times(1);

        refreshedToken.Should().BeOfType<CredentialToken>();
        refreshedToken.AccessToken.Should().Be(expectedToken.AccessToken);
        refreshedToken.RefreshToken.Should().Be(expectedToken.RefreshToken);
        refreshedToken.ExpiresAt.Should().Be(refreshedToken.ReceivedAt.Plus(Duration.FromSeconds(expectedToken.ExpiresIn)));
        refreshedToken.Should().NotBe(oldToken);
    }

    public record TokenResponse(string AccessToken, uint ExpiresIn, string RefreshToken)
    {
        public override string ToString() => $"{{ access_token = {AccessToken}, expires_in = {ExpiresIn}, refresh_token = {RefreshToken} }}";
    }
}