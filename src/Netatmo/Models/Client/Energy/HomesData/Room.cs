using Newtonsoft.Json;

namespace Netatmo.Models.Client.Energy.HomesData
{
    public class Room
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("measure_offset_NAPlug_estimated_temperature")]
        public double MeasureOffsetNaPlugEstimatedTemperature { get; set; }

        [JsonProperty("measure_offset_NAPlug_temperature")]
        public double MeasureOffsetNaPlugTemperature { get; set; }

        [JsonProperty("module_ids")]
        public string[] ModuleIds { get; set; }
    }
}