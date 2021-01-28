using System;
using System.Collections;
using System.Collections.Generic;
using Netatmo.Models.Client.Energy;
using NodaTime;

namespace Netatmo.Tests.ClassData.Energy
{
    public class GetRoomMeasureArgumentExceptionData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new GetRoomMeasureParameters(), "Home Id shouldn't be null" };
            yield return new object[] { new GetRoomMeasureParameters { HomeId = "5a327cbdb05a2133678b5d3e" }, "Room Id shouldn't be null" };
            yield return new object[] { new GetRoomMeasureParameters { HomeId = "5a327cbdb05a2133678b5d3e", RoomId = "2255031728" }, "Scale shouldn't be null" };
            yield return new object[] { new GetRoomMeasureParameters { HomeId = "5a327cbdb05a2133678b5d3e", RoomId = "2255031728", Scale = Scale.Max }, "Type shouldn't be null" };
            yield return new object[]
            {
                new GetRoomMeasureParameters { HomeId = "5a327cbdb05a2133678b5d3e", RoomId = "2255031728", Scale = Scale.Max, Type = ThermostatMeasurementType.DateMinTemp },
                "Type shouldn't be allow for this scale"
            };
            yield return new object[]
            {
                new GetRoomMeasureParameters
                {
                    HomeId = "5a327cbdb05a2133678b5d3e",
                    RoomId = "2255031728",
                    Scale = Scale.Max,
                    Type = ThermostatMeasurementType.Temperature,
                    Limit = 2000
                },
                "Limit should be between 0 and 1024"
            };
            yield return new object[]
            {
                new GetRoomMeasureParameters
                {
                    HomeId = "5a327cbdb05a2133678b5d3e",
                    RoomId = "2255031728",
                    Scale = Scale.Max,
                    Type = ThermostatMeasurementType.Temperature,
                    Limit = -42
                },
                "Limit should be between 0 and 1024"
            };
            yield return new object[]
            {
                new GetRoomMeasureParameters
                {
                    HomeId = "5a327cbdb05a2133678b5d3e",
                    RoomId = "2255031728",
                    Scale = Scale.Max,
                    Type = ThermostatMeasurementType.Temperature,
                    BeginAt = Instant.FromDateTimeUtc(DateTime.SpecifyKind(new DateTime(2018, 4, 30), DateTimeKind.Utc)),
                    EndAt = Instant.FromDateTimeUtc(DateTime.SpecifyKind(new DateTime(2017, 4, 30), DateTimeKind.Utc))
                },
                "BeginAt should be lower than EndAt"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}