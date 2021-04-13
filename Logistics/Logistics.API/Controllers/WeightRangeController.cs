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
    [Route("api/weight-range")]
    public class WeightRangeController : ControllerBase
    {
        private readonly IWeightRangeService _weight;

        public WeightRangeController(IWeightRangeService weight)
        {
            _weight = weight;
        }

        [HttpGet]
        public async Task<ActionResult<WeightRangeResponse>> GetWeightRange()
        {
            var weight = await _weight.BrowseAsync();

            if (weight.Count == 0)
                return NoContent();
            return Ok(weight);
        }

        [HttpGet("{weightId}")]
        public async Task<ActionResult<WeightRangeResponse>> GetWeightRangeById(Guid weightId)
        {
            var weight = await _weight.FindAsync(weightId);

            if (weight == null)
                return NoContent();
            return Ok(weight);
        }

        [HttpPost]
        public async Task<ActionResult<WeightRangeResponse>> PostWeightRange([FromBody] WeightRangePostBody weightRange)
        {
            var weight = await _weight.CreateAsync(weightRange);
            return CreatedAtAction(nameof(GetWeightRangeById), new { weightId = weight.Id }, weight);
        }

        [HttpPut("{weightId}")]
        public async Task<ActionResult<WeightRangeResponse>> PutWeightRange(Guid weightId, [FromBody] WeightRangePutBody weightRange)
        {
            var weight = await _weight.UpdateAsync(weightId, weightRange);
            if (weight == null)
                return BadRequest("WeightRange with that Id doesn't exist");

            return Ok(weight);
        }

        [HttpDelete("{weightId}")]
        public async Task<IActionResult> DeleteWeithRange(Guid weightId)
        {
            await _weight.DeleteAsync(weightId);
            return NoContent();
        }
    }
}
