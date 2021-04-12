using Logistics.API.Interfaces;
using Logistics.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Controllers
{
    [ApiController]
    [Route("api/distance-price")]
    public class DistancePriceController : ControllerBase
    {
        private readonly IDistancePrice _distance;

        public DistancePriceController(IDistancePrice distance)
        {
            _distance = distance;
        }

        [HttpGet]
        public async Task<ActionResult<DistancePriceResponse>> GetDistancePrice()
        {
            var distances = await _distance.BrowseAsync();
            if (distances.Count == 0)
                return NoContent();

            return Ok(distances);
        }

        [HttpGet("{distanceId}")]
        public async Task<ActionResult<DistancePriceResponse>> GetDistancePriceById(Guid distanceId)
        {
            var distance = await _distance.FindAsync(distanceId);

            if (distance == null)
                return NoContent();

            return Ok(distance);
        }

        [HttpPost]
        public async Task<ActionResult<DistancePriceResponse>> PostDistancePrice([FromBody] DistancePricePostBody distancePrice)
        {
            var distance = await _distance.CreateAsync(distancePrice);

            return CreatedAtAction(nameof(GetDistancePriceById), new { distanceId = distance.Id }, distance);
        }

        [HttpPut("{distanceId}")]
        public async Task<ActionResult<DistancePriceResponse>> PutDistancePrice(Guid distanceId, [FromBody] DistancePricePutBody distancePrice)
        {
            var distance = await _distance.UpdateAsync(distanceId, distancePrice);

            if (distance == null)
                return BadRequest("DistancePrice with that id doesn't exits");

            return Ok(distance);
        }

        [HttpDelete("{distanceId}")]
        public async Task<IActionResult> DeleteDistancePrice(Guid distance)
        {
            await _distance.DeleteAsync(distance);
            return NoContent();
        }
    }
}
