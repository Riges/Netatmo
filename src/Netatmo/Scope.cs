namespace Netatmo;

public class Scope : IEquatable<Scope>
{
    private Scope(string value)
    {
        Value = value;
    }

    public string Value { get; }
    public static Scope StationRead => new Scope("read_station");
    public static Scope ThermostatRead => new Scope("read_thermostat");
    public static Scope StationWrite => new Scope("write_thermostat");
    public static Scope CameraRead => new Scope("read_camera");
    public static Scope CameraWrite => new Scope("write_camera");
    public static Scope CameraAccess => new Scope("access_camera");
    public static Scope PresenceRead => new Scope("read_presence");
    public static Scope PresenceAccess => new Scope("access_presence");
    public static Scope HomecoachRead => new Scope("read_homecoach");

    public bool Equals(Scope other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return string.Equals(Value, other.Value);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Scope) obj);
    }

    public override int GetHashCode()
    {
        return Value != null ? Value.GetHashCode() : 0;
    }

    public static bool operator ==(Scope obj1, Scope obj2)
    {
        if (ReferenceEquals(obj1, obj2)) return true;

        if (ReferenceEquals(obj1, null)) return false;
        if (ReferenceEquals(obj2, null)) return false;

        return obj1.Equals(obj2);
    }

    public static bool operator !=(Scope obj1, Scope obj2)
    {
        return !(obj1 == obj2);
    }
}