using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Flurl.Http.Testing;
using Netatmo.Models;
using NodaTime;
using NodaTime.Testing;
using Xunit;

namespace Netatmo.Tests
{
    public class CredentialManager : IDisposable
    {
        private readonly HttpTest httpTest;

        public CredentialManager()
        {
            httpTest = new HttpTest();
        }

        public void Dispose()
        {
            httpTest.Dispose();
        }

        [Fact]
        public async Task GenerateToken_Should_Acquire_Excepted_CredentialToken()
        {
            var expectedToken = new { access_token = "2YotnFZFEjr1zCsicMWpAA", expires_in = 10800, refresh_token = "tGzv3JOkF0XG5Qx2TlKWIA" };

            httpTest.RespondWithJson(expectedToken);

            var sut = new Netatmo.CredentialManager("https://api.netatmo.com/", "clientId", "clientSecret", SystemClock.Instance);

            await sut.GenerateToken("username@email.com", "p@$$W0rd",
                new[]
                {
                    Scope.CameraAccess, Scope.CameraRead, Scope.CameraWrite, Scope.HomecoachRead, Scope.PresenceAccess, Scope.PresenceRead, Scope.StationRead,
                    Scope.StationWrite, Scope.ThermostatRead
                });

            var token = sut.CredentialToken;

            httpTest
                .ShouldHaveCalled("https://api.netatmo.com/oauth2/token")
                .WithVerb(HttpMethod.Post)
                .WithContentType("application/x-www-form-urlencoded")
                .WithRequestBody(
                    "grant_type=password&client_id=clientId&client_secret=clientSecret&username=username%40email.com&password=p%40%24%24W0rd&scope=access_camera+read_camera+write_camera+read_homecoach+access_presence+read_presence+read_station+write_thermostat+read_thermostat")
                .Times(1);

            token.Should().BeOfType<CredentialToken>();
            token.AccessToken.Should().Be(expectedToken.access_token);
            token.RefreshToken.Should().Be(expectedToken.refresh_token);
            token.ExpiresAt.Should().Be(token.ReceivedAt.Plus(Duration.FromSeconds(expectedToken.expires_in)));
        }

        [Fact]
        public void ProvideOAuth2Token_Should_Provide_Token_From_Existing()
        {
            var expectedToken = new { access_token = "2YotnFZFEjr1zCsicMWpAA", expires_in = 20 };

            httpTest.RespondWithJson(expectedToken);

            var sut = new Netatmo.CredentialManager("https://api.netatmo.com/", "clientId", "clientSecret", SystemClock.Instance);

            sut.ProvideOAuth2Token(expectedToken.access_token);

            var token = sut.CredentialToken;

            token.Should().BeOfType<CredentialToken>();
            token.AccessToken.Should().Be(expectedToken.access_token);
            token.RefreshToken.Should().BeNull();
            token.ExpiresAt.Should().Be(token.ReceivedAt.Plus(Duration.FromSeconds(expectedToken.expires_in)));
        }

        [Fact]
        public async Task RefreshToken_Should_Refresh_Token()
        {
            var token = new { access_token = "2YotnFZFEjr1zCsicMWpAA", expires_in = 10800, refresh_token = "tGzv3JOkF0XG5Qx2TlKWIA" };

            var expectedToken = new { access_token = "dGVzdGNsaWVudDpzZWNyZXQ", expires_in = 10800, refresh_token = "fdb8fdbecf1d03ce5e6125c067733c0d51de209c" };

            httpTest.RespondWithJson(token);
            httpTest.RespondWithJson(expectedToken);

            var fakeClock = new FakeClock(SystemClock.Instance.GetCurrentInstant(), Duration.FromMinutes(-2));

            var sut = new Netatmo.CredentialManager("https://api.netatmo.com/", "clientId", "clientSecret", fakeClock);

            await sut.GenerateToken("username@email.com", "p@$$W0rd");
            var oldToken = sut.CredentialToken;
            await sut.RefreshToken();
            var refreshedToken = sut.CredentialToken;

            httpTest
                .ShouldHaveCalled("https://api.netatmo.com/oauth2/token")
                .WithVerb(HttpMethod.Post)
                .WithContentType("application/x-www-form-urlencoded")
                .WithRequestBody(
                    $"grant_type=refresh_token&client_id=clientId&client_secret=clientSecret&refresh_token={oldToken.RefreshToken}")
                .Times(1);

            refreshedToken.Should().BeOfType<CredentialToken>();
            refreshedToken.AccessToken.Should().Be(expectedToken.access_token);
            refreshedToken.RefreshToken.Should().Be(expectedToken.refresh_token);
            refreshedToken.ExpiresAt.Should().Be(refreshedToken.ReceivedAt.Plus(Duration.FromSeconds(expectedToken.expires_in)));
            refreshedToken.Should().NotBe(oldToken);
        }
    }
}