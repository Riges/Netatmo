namespace Netatmo;

public class Scale : IEquatable<Scale>
{
    private Scale(string value)
    {
        Value = value;
    }

    public string Value { get; }
    public static Scale Max => new Scale("max");
    public static Scale HalfHour => new Scale("30min");
    public static Scale OneHour => new Scale("1hour");
    public static Scale ThreeHours => new Scale("3hours");
    public static Scale OneDay => new Scale("1day");
    public static Scale OneWeek => new Scale("1week");
    public static Scale OneMonth => new Scale("1month");

    public bool Equals(Scale other)
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
        return Equals((Scale) obj);
    }

    public override int GetHashCode()
    {
        return Value != null ? Value.GetHashCode() : 0;
    }

    public static bool operator ==(Scale obj1, Scale obj2)
    {
        if (ReferenceEquals(obj1, obj2)) return true;

        if (ReferenceEquals(obj1, null)) return false;
        if (ReferenceEquals(obj2, null)) return false;

        return obj1.Equals(obj2);
    }

    public static bool operator !=(Scale obj1, Scale obj2)
    {
        return !(obj1 == obj2);
    }
}