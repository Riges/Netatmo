using System.Text.Json.Serialization;
using Netatmo.Converters;
using Netatmo.Models.Client.Energy.Enums;

namespace Netatmo.Models.Client.Energy;

public class Zone
{
    public Zone()
    {
        Rooms = new List<Room>();
    }

    public Zone(string id, string name, ZoneTypeEnum type, Room[] rooms = null)
        : this()
    {
        Id = id;
        Name = name;
        Type = type;

        if (rooms != null)
        {
            Rooms.AddRange(rooms);
        }
    }

    [JsonPropertyName("id")]
    [JsonConverter(typeof(ZoneIdConverter))]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("type")]
    public ZoneTypeEnum Type { get; set; }

    [JsonPropertyName("rooms")]
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

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("therm_setpoint_temperature")]
        public double ThermSetPointTemperature { get; set; }
    }
}