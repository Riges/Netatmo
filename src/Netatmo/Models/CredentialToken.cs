using Netatmo.Models.Client;
using NodaTime;

namespace Netatmo.Models
{
    public class CredentialToken
    {
        public int ExpiresIn { get; }
        
        public string AccessToken { get; }
        
        public string RefreshToken { get; }
        
        public Instant ReceivedAt { get; }

        public Instant ExpiresAt => ReceivedAt.Plus(Duration.FromSeconds(ExpiresIn));
        
        public CredentialToken(Token token, IClock clock)
        {
            ReceivedAt = clock.GetCurrentInstant();
            ExpiresIn = token.ExpiresIn;
            AccessToken = token.AccessToken;
            RefreshToken = token.RefreshToken;
        }
    }
}