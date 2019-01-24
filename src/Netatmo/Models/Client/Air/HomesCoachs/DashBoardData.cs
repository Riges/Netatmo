using Newtonsoft.Json;
using NodaTime;

namespace Netatmo.Models.Client.Air.HomesCoachs
{
    public class DashBoardData
    {
        [JsonProperty("time_utc")]
        Instant TimeUtc { get; set; }
        
        [JsonProperty("Temperature")]
        float Temperature {get;set;}
        
        [JsonProperty("CO2")]
        int CO2 {get;set;}
        
        [JsonProperty("Humidity")]
        int Humidity {get;set;}
        
        [JsonProperty("Noise")]
        float Noise {get;set;}
        
        [JsonProperty("Pressure")]
        float Pressure {get;set;}
        
        [JsonProperty("AbsolutePressure")]
        float AbsolutePressure {get;set;}
        
        [JsonProperty("health_idx")]
        float HealthIdx {get;set;}
        
        [JsonProperty("min_temp")]
        float MinTemp {get;set;}
        
        [JsonProperty("max_temp")]
        float MaxTemp {get;set;}
        
        [JsonProperty("date_min_temp")]
        Instant DateMinTemp {get;set;}
        
        [JsonProperty("date_max_temp")]
        Instant DateMaxTemp {get;set;}
        
        
        
    }
}