namespace Netatmo
{
    public class Scope
    {
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
        
        private Scope(string value) { Value = value; }
    }
}