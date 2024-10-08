using NodaTime;

namespace Netatmo.Models.Client.Energy;

public class GetRoomMeasureParameters
{
    public string HomeId { get; set; }
    public string RoomId { get; set; }
    public Scale Scale { get; set; }
    public ThermostatMeasurementType Type { get; set; }
    public Instant? BeginAt { get; set; }
    public Instant? EndAt { get; set; }
    public int? Limit { get; set; }
    public bool? Optimize { get; set; }
    public bool? RealTime { get; set; }
}