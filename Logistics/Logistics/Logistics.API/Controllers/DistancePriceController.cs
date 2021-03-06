﻿using Logistics.API.Interfaces;
using Logistics.API.Models.DistancePriceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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

        /// <summary>
        /// Get all DistancePrices
        /// </summary>
        /// <returns>Return list of DistancePrices</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<ActionResult<DistancePriceOverview>> GetDistancePrice()
        {
            var distances = await _distance.BrowseAsync();

            if (distances.Count == 0)
            {
                return NoContent();
            }

            return Ok(distances);
        }

        /// <summary>
        /// Get DistancePrice by Id
        /// </summary>
        /// <param name="distanceId">Id of DistancePrice</param>
        /// <returns>Distance with "distanceId"</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin,User")]
        [HttpGet("{distanceId}")]
        public async Task<ActionResult<DistancePriceDetails>> GetDistancePriceById(Guid distanceId)
        {
            var distance = await _distance.FindAsync(distanceId);

            return Ok(distance);
        }

        /// <summary>
        /// Post DistancePrice
        /// </summary>
        /// <param name="distancePrice">New DistancePrice body</param>
        /// <returns>Created DistancePrice with 201</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<DistancePriceConfirmation>> PostDistancePrice([FromBody] DistancePricePostBody distancePrice)
        {
            var newDistancePrice = await _distance.CreateAsync(distancePrice);

            return CreatedAtAction(nameof(GetDistancePriceById), new { distanceId = newDistancePrice.Id }, newDistancePrice);
        }

        /// <summary>
        /// Update DistancePrice
        /// </summary>
        /// <param name="distanceId">Id of updated DistancePrice</param>
        /// <param name="distancePrice">New DistancePrice body</param>
        /// <returns>Updated DistancePrice</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin")]
        [HttpPut("{distanceId}")]
        public async Task<ActionResult<DistancePriceConfirmation>> PutDistancePrice(Guid distanceId, [FromBody] DistancePricePutBody distancePrice)
        {
            var updatedDistancePrice = await _distance.UpdateAsync(distanceId, distancePrice);

            return Ok(updatedDistancePrice);
        }

        /// <summary>
        /// Delete DistancePrice
        /// </summary>
        /// <param name="distanceId">Id of deleted DistancePrice</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("{distanceId}")]
        public async Task<IActionResult> DeleteDistancePrice(Guid distanceId)
        {
            await _distance.DeleteAsync(distanceId);

            return NoContent();
        }
    }
}
