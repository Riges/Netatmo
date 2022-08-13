using Netatmo.Models.Client;
using NodaTime;

namespace Netatmo.Models;

public record CredentialToken(int ExpiresIn, string AccessToken, string RefreshToken, Instant ReceivedAt)
{
    public CredentialToken(Token token, IClock clock)
        : this(token.ExpiresIn, token.AccessToken, token.RefreshToken, clock.GetCurrentInstant())
    {
    }

    public Instant ExpiresAt => ReceivedAt.Plus(Duration.FromSeconds(ExpiresIn));
}