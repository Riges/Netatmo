using System.Collections.Generic;
using Netatmo.Models.Client.Energy.Enums;
using Newtonsoft.Json;

namespace Netatmo.Models.Client.Energy
{
    public class Zone
    {
        public Zone()
        {
            Rooms = new List<Room>();
        }

        public Zone(string id, string name, ZoneTypeEnum type, Room[] rooms = null) : this()
        {
            Id = id;
            Name = name;
            Type = type;

            if (rooms != null) Rooms.AddRange(rooms);
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public ZoneTypeEnum Type { get; set; }

        [JsonProperty("rooms")]
        public List<Room> Rooms { get; set; }

        public class Room
        {
            public Room()
            {
            }

            public Room(string id, double thermSetPointTemperature)
            {
                Id = id;
                ThermSetPointTemperature = thermSetPointTemperature;
            }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("therm_setpoint_temperature")]
            public double ThermSetPointTemperature { get; set; }
        }
    }
}