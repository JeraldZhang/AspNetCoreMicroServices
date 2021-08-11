using System;
using LocationReporter.Converters;
using LocationReporter.Events;
using LocationReporter.Models;
using Xunit;

namespace LocationReportService.Tests.Converters
{
    public class CommandEventConverterTests
    {
        [Fact]
        public void CommandToEvent_AugmentsCommandWithTimestamp()
        {
            var startTime = DateTime.Now.ToUniversalTime().Ticks;
            var command = new LocationReport
            {
                Latitude = 10.0,
                Longitude = 30.0,
                Origin = "TESTS",
                MemberID = Guid.NewGuid()
            };
            var converter = new CommandEventConverter();
            var recordedEvent = converter.CommandToEvent(command);

            Assert.Equal(command.Latitude, recordedEvent.Latitude);
            Assert.Equal(command.Longitude, recordedEvent.Longitude);
            Assert.Equal(command.Origin, recordedEvent.Origin);
            Assert.True(recordedEvent.RecordedTime >= startTime);
        }
    }
}