using System;
using Microsoft.AspNetCore.Mvc;
using LocationReporter.Events;
using LocationReporter.Models;
using LocationReporter.Converters;
using LocationReporter.Services;

namespace LocationReporter.Controllers
{
    [Route("/api/members/{memberId}/locationreports")]
    public class LocationReportsController : Controller
    {
        private ICommandEventConverter _converter;
        private IEventEmitter _eventEmitter;
        private ITeamServiceClient _teamServiceClient;
        

        public LocationReportsController(ICommandEventConverter converter, 
            IEventEmitter eventEmitter, 
            ITeamServiceClient teamServiceClient) {
            this._converter = converter;
            this._eventEmitter = eventEmitter;
            this._teamServiceClient = teamServiceClient;
        }

        [HttpPost]
        public ActionResult PostLocationReport(Guid memberId, [FromBody]LocationReport locationReport)
        {
            MemberLocationRecordedEvent locationRecordedEvent = _converter.CommandToEvent(locationReport);
            locationRecordedEvent.TeamID = _teamServiceClient.GetTeamForMember(locationReport.MemberID);
            _eventEmitter.EmitLocationRecordedEvent(locationRecordedEvent);

            return this.Created($"/api/members/{memberId}/locationreports/{locationReport.ReportID}", locationReport);
        }
    }
}