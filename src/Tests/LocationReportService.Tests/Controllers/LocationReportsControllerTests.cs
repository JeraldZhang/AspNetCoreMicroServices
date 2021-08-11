using System;
using Microsoft.AspNetCore.Mvc;
using LocationReporter.Events;
using LocationReporter.Models;
using LocationReporter.Converters;
using LocationReporter.Services;
using Moq;
using Xunit;

namespace LocationReporter.Controllers
{
    public class LocationReportsControllerTests
    {
        [Fact]
        public void PostLocationReport_GenerateEvent()
        {
            // Arrange
            var memeberId = Guid.NewGuid();
            var reportID = Guid.NewGuid();
            var locationReport = new LocationReport
            {
                MemberID = memeberId,
                ReportID = reportID,
            };
            var memberLocationRecordedEvent = new MemberLocationRecordedEvent();

            var mockConverter = new Mock<ICommandEventConverter>();
            var mockEventEmitter = new Mock<IEventEmitter>();
            var mockTSClient = new Mock<ITeamServiceClient>();
            var sequence = new MockSequence();

            mockConverter
                .InSequence(sequence)
                .Setup(ceConvert => ceConvert.CommandToEvent(
                    It.Is<LocationReport>(lr => lr.MemberID == memeberId && lr.ReportID == reportID)))
                .Returns(memberLocationRecordedEvent);

            mockTSClient
                .InSequence(sequence)
                .Setup(mtsct => mtsct.GetTeamForMember(It.Is<Guid>(g => g == memeberId)))
                .Returns(memeberId);

            // Act
            var controller = new LocationReportsController(
                mockConverter.Object, mockEventEmitter.Object, mockTSClient.Object);
            var actionResult = controller.PostLocationReport(memeberId, locationReport);

            // Assert
            Assert.Equal(memeberId, memberLocationRecordedEvent.TeamID);
            mockConverter.Verify(mc => mc.CommandToEvent(It.IsAny<LocationReport>()), Times.Once);
            mockTSClient.Verify(mtsc => mtsc.GetTeamForMember(It.IsAny<Guid>()), Times.Once);
        }
    }
}