using System;
using System.Collections.Generic;
using System.Linq;

namespace Netatmo
{
    public class ThermostatMeasurementType : IEquatable<ThermostatMeasurementType>
    {
        private ThermostatMeasurementType(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ThermostatMeasurementType Temperature => new ThermostatMeasurementType("temperature");
        public static ThermostatMeasurementType SetPointTemperature => new ThermostatMeasurementType("sp_temperature");
        public static ThermostatMeasurementType BoilerOn => new ThermostatMeasurementType("boileron");
        public static ThermostatMeasurementType BoilerOff => new ThermostatMeasurementType("boileroff");
        public static ThermostatMeasurementType MinTemp => new ThermostatMeasurementType("min_temp");
        public static ThermostatMeasurementType MaxTemp => new ThermostatMeasurementType("max_temp");
        public static ThermostatMeasurementType SumBoilerOn => new ThermostatMeasurementType("sum_boiler_on");
        public static ThermostatMeasurementType SumBoilerOff => new ThermostatMeasurementType("sum_boiler_off");
        public static ThermostatMeasurementType DateMinTemp => new ThermostatMeasurementType("date_min_temp");

        public bool Equals(ThermostatMeasurementType other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Value, other.Value);
        }

        public static List<ThermostatMeasurementType> AvailableTypes(Scale scale)
        {
            if (new[] {Scale.HalfHour, Scale.OneHour, Scale.ThreeHours}.Any(s => s.Value == scale.Value))
                return new List<ThermostatMeasurementType> {Temperature, SetPointTemperature, MinTemp, MaxTemp, SumBoilerOn, SumBoilerOff};

            if (new[] {Scale.OneDay, Scale.OneWeek, Scale.OneMonth}.Any(s => s.Value == scale.Value))
                return new List<ThermostatMeasurementType> {Temperature, DateMinTemp, MinTemp, MaxTemp, SumBoilerOn, SumBoilerOff};

            return new List<ThermostatMeasurementType> {Temperature, SetPointTemperature, BoilerOn, BoilerOff};
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ThermostatMeasurementType) obj);
        }

        public override int GetHashCode()
        {
            return Value != null ? Value.GetHashCode() : 0;
        }

        public static bool operator ==(ThermostatMeasurementType obj1, ThermostatMeasurementType obj2)
        {
            if (ReferenceEquals(obj1, obj2)) return true;

            if (ReferenceEquals(obj1, null)) return false;
            if (ReferenceEquals(obj2, null)) return false;

            return obj1.Equals(obj2);
        }

        public static bool operator !=(ThermostatMeasurementType obj1, ThermostatMeasurementType obj2)
        {
            return !(obj1 == obj2);
        }
    }
}