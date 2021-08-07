using LocationReporter.Events;
using LocationReporter.Models;

namespace LocationReporter.Converters
{
    public interface ICommandEventConverter
    {
        MemberLocationRecordedEvent CommandToEvent(LocationReport locationReport); 
    }
}