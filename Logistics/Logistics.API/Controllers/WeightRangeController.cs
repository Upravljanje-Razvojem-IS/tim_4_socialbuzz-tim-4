﻿using Logistics.API.Interfaces;
using Logistics.API.Models.WeightRangeModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Get all WeightRanges
        /// </summary>
        /// <returns>List of WeightRanges</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<ActionResult<WeightRangeOverview>> GetWeightRange()
        {
            var weight = await _weight.BrowseAsync();

            if (weight.Count == 0)
                return NoContent();
            return Ok(weight);
        }

        /// <summary>
        /// Get WeightRange by Id
        /// </summary>
        /// <param name="weightId">Id of WeightRange</param>
        /// <returns>WeightRange with "weightId"</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin,User")]
        [HttpGet("{weightId}")]
        public async Task<ActionResult<WeightRangeOverview>> GetWeightRangeById(Guid weightId)
        {
            var weight = await _weight.FindAsync(weightId);

            if (weight == null)
                return NotFound();
            return Ok(weight);
        }

        /// <summary>
        /// Post new WeightRange
        /// </summary>
        /// <param name="weightRange">Body of new WeightRange</param>
        /// <returns>Created WeightRange with 201</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<WeightRangeOverview>> PostWeightRange([FromBody] WeightRangePostBody weightRange)
        {
            var weight = await _weight.CreateAsync(weightRange);
            return CreatedAtAction(nameof(GetWeightRangeById), new { weightId = weight.Id }, weight);
        }

        /// <summary>
        /// Update WeightRange
        /// </summary>
        /// <param name="weightId">Id of updated WeightRange</param>
        /// <param name="weightRange">New body of WeightRange</param>
        /// <returns>New WeightRange</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin")]
        [HttpPut("{weightId}")]
        public async Task<ActionResult<WeightRangeOverview>> PutWeightRange(Guid weightId, [FromBody] WeightRangePutBody weightRange)
        {
            var weight = await _weight.UpdateAsync(weightId, weightRange);
            if (weight == null)
                return BadRequest("WeightRange with that Id doesn't exist");

            return Ok(weight);
        }

        /// <summary>
        /// Delete WeightRange
        /// </summary>
        /// <param name="weightId">Id of deleted WeightRange</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("{weightId}")]
        public async Task<IActionResult> DeleteWeithRange(Guid weightId)
        {
            await _weight.DeleteAsync(weightId);
            return NoContent();
        }
    }
}
