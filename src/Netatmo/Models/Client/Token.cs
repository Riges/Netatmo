using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Netatmo.Models.Client
{
    public class Token
    {
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}