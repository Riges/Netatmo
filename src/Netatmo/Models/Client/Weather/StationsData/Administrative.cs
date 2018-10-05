using Netatmo.Models.Client.Enums;
using Newtonsoft.Json;

namespace Netatmo.Models.Client.Weather.StationsData
{
    public class Administrative
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("feel_like_algo")]
        public FeelLikeAlgoEnum FeelLikeAlgo { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("pressureunit")]
        public PressureUnitEnum PressureUnit { get; set; }

        [JsonProperty("reg_locale")]
        public string RegLocale { get; set; }

        [JsonProperty("unit")]
        public UnitEnum Unit { get; set; }

        [JsonProperty("windunit")]
        public WindUnitEnum WindUnit { get; set; }
    }
}