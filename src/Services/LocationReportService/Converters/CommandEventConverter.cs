using System;
using LocationReporter.Events;
using LocationReporter.Models;

namespace LocationReporter.Converters
{
    public class CommandEventConverter : ICommandEventConverter
    {
        public MemberLocationRecordedEvent CommandToEvent(LocationReport locationReport) 
        {
            var locationRecordedEvent = new MemberLocationRecordedEvent {
                Latitude = locationReport.Latitude,
                Longitude = locationReport.Longitude,
                Origin = locationReport.Origin,
                MemberID = locationReport.MemberID,
                ReportID = locationReport.ReportID,
                RecordedTime = DateTime.Now.ToUniversalTime().Ticks
            };

           return locationRecordedEvent;
        }
    }
}