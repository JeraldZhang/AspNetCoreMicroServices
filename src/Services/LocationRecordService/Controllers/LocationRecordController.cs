using Microsoft.AspNetCore.Mvc;
using System;
using LocationService.Models;
using LocationService.Persisitence;

namespace LocationService.Controllers
{
    [ApiController]
    [Route("locations/{memeberId}")]
    public class LocationRecordController : ControllerBase
    {
        private readonly ILocationRecordRepository _locationRepository;

        public LocationRecordController(ILocationRecordRepository repository)
        {
            _locationRepository = repository ??
                throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost]
        public IActionResult AddLocation(Guid memeberId, [FromBody] LocationRecord locationRecord)
        {
            _locationRepository.Add(locationRecord);
            return Created($"/locations/{memeberId}/{locationRecord.ID}", locationRecord);
        }

        [HttpPost]
        public IActionResult GetLocationForMemeber(Guid memeberId)
        {
            return Ok(_locationRepository.GetLatestForMemeber(memeberId));
        }

        [HttpGet("latest")]
        public IActionResult GetLatestForMemeber(Guid memeberId)
        {
            return Ok(_locationRepository.GetLatestForMemeber(memeberId));
        }
    }
}
