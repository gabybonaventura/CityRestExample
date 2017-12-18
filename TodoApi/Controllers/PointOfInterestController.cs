using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TodoApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/cities")]
    public class PointOfInterestController : Controller
    {
        private ILogger<PointOfInterestController> _logger;
        private IMailService _mailServices;

        public PointOfInterestController(ILogger<PointOfInterestController> logger,
                                         IMailService mailService)
        {
            _logger = logger;
            _mailServices = mailService;
        }

        [HttpGet("{cityId}/pointofinterest")]
        public IActionResult getPointsOfInterest(int cityId)
        {
            try
            {
                var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
                if (city == null)
                {
                    _logger.LogInformation($"City with id {cityId} wasn't found where accesing point of interest.");
                    return NotFound();
                }

                return Ok(city.PointOfInterest);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting point of interest for city with id:  {cityId}.", ex);
                return StatusCode(500, "A problem happend while handling your request.");
            }

        }

        [HttpGet("{cityId}/pointofinterest/{id}", Name = "GetPointOfInterest")]
        public IActionResult getPointOfInterest(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();
            var pointOfInterest = city.PointOfInterest.FirstOrDefault(c => c.Id == id);
            if (pointOfInterest == null)
                return NotFound();
            return Ok(pointOfInterest);
        }

        [HttpPost("{cityId}/pointofinterest")]
        public IActionResult createPointOfInteres(int cityId,
                                                  [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            if (pointOfInterest == null)
                return BadRequest();
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (city == null)
                return NotFound();
            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(
                c => c.PointOfInterest).Max(p => p.Id);
            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };
            city.PointOfInterest.Add(finalPointOfInterest);
            return CreatedAtRoute("GetPointOfInterest", new
            { cityId = cityId, id = finalPointOfInterest.Id }, finalPointOfInterest);
        }

        [HttpPut("{cityId}/pointofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id,
                                                   [FromBody] PointOfInterestForUpdateDto pointOfInterest)
        {
            if (pointOfInterest == null)
                return BadRequest();
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (city == null)
                return NotFound();
            var pointOfInterestFromDataStore = city.PointOfInterest.FirstOrDefault(p => p.Id == id);
            if (pointOfInterestFromDataStore == null)
                return NotFound();
            pointOfInterestFromDataStore.Name = pointOfInterest.Name;
            pointOfInterestFromDataStore.Description = pointOfInterest.Description;
            return NoContent();
        }

        [HttpPatch("{cityId}/pointofinterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id,
            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();
            var pointOfInterestFromStore = city.PointOfInterest.FirstOrDefault(p => p.Id == id);
            if (pointOfInterestFromStore == null)
                return NotFound();
            var pointOfInterestToPatch =
                new PointOfInterestForUpdateDto()
                {
                    Name = pointOfInterestFromStore.Name,
                    Description =pointOfInterestFromStore.Description
                };
            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            TryValidateModel(pointOfInterestToPatch);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;
            return NoContent();
        }

        [HttpDelete("{cityId}/pointofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();
            var pointOfInterestFromStore = city.PointOfInterest.FirstOrDefault(p => p.Id == id);
            if (pointOfInterestFromStore == null)
                return NotFound();
            city.PointOfInterest.Remove(pointOfInterestFromStore);

            _mailServices.Send("Point of interest deleted.",
                               $"Point of interest {pointOfInterestFromStore.Name} with id {pointOfInterestFromStore.Id} was deleted");

            return NoContent();
        }
    }
}
