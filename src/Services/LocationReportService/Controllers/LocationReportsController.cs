using System;
using Microsoft.AspNetCore.Mvc;
using LocationReporter.Models;
using LocationReporter.Converters;
using LocationReporter.Services;
using System.Threading.Tasks;

namespace LocationReporter.Controllers
{
    [Route("/api/members/{memberId}/locationreports")]
    public class LocationReportsController : Controller
    {
        private readonly ICommandEventConverter _converter;
        private readonly IEventEmitter _eventEmitter;
        private readonly ITeamServiceClient _teamServiceClient;

        public LocationReportsController(ICommandEventConverter converter,
            IEventEmitter eventEmitter,
            ITeamServiceClient teamServiceClient)
        {
            this._converter = converter;
            this._eventEmitter = eventEmitter;
            this._teamServiceClient = teamServiceClient;
        }

        [HttpPost]
        public async Task<ActionResult> PostLocationReport(Guid memberId, [FromBody] LocationReport locationReport)
        {
            var locationRecordedEvent = _converter.CommandToEvent(locationReport);
            locationRecordedEvent.TeamID = await _teamServiceClient.GetTeamForMember(locationReport.MemberID);
            _eventEmitter.EmitLocationRecordedEvent(locationRecordedEvent);

            return this.Created($"/api/members/{memberId}/locationreports/{locationReport.ReportID}", locationReport);
        }
    }
}