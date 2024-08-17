namespace Netatmo;

public class Scope : IEquatable<Scope>
{
    private Scope(string value)
    {
        Value = value;
    }

    public string Value { get; }
    public static Scope StationRead => new("read_station");
    public static Scope ThermostatRead => new("read_thermostat");
    public static Scope StationWrite => new("write_thermostat");
    public static Scope CameraRead => new("read_camera");
    public static Scope CameraWrite => new("write_camera");
    public static Scope CameraAccess => new("access_camera");
    public static Scope PresenceRead => new("read_presence");
    public static Scope PresenceAccess => new("access_presence");
    public static Scope HomecoachRead => new("read_homecoach");

    public bool Equals(Scope other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return string.Equals(Value, other.Value);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((Scope)obj);
    }

    public override int GetHashCode() => Value != null ? Value.GetHashCode() : 0;

    public static bool operator ==(Scope obj1, Scope obj2)
    {
        if (ReferenceEquals(obj1, obj2))
        {
            return true;
        }

        if (ReferenceEquals(obj1, null))
        {
            return false;
        }

        if (ReferenceEquals(obj2, null))
        {
            return false;
        }

        return obj1.Equals(obj2);
    }

    public static bool operator !=(Scope obj1, Scope obj2) => !(obj1 == obj2);
}