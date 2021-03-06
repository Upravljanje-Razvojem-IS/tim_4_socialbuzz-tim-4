﻿using Logistics.API.Interfaces;
using Logistics.API.Models.CityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Logistics.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _city;

        public CityController(ICityService city)
        {
            _city = city;
        }

        /// <summary>
        /// Get all cities
        /// </summary>
        /// <returns>Return list of cities</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<ActionResult<CityOverview>> GetCities()
        {
            var cities = await _city.BrowseAsync();

            if (cities.Count == 0)
            {
                return NoContent();
            }

            return Ok(cities);
        }

        /// <summary>
        /// Get city with "cityId"
        /// </summary>
        /// <param name="cityId">Id of returned city</param>
        /// <returns>Returns city with "cityId"</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin,User")]
        [HttpGet("{cityId}")]
        public async Task<ActionResult<CityDetails>> GetCityById(Guid cityId)
        {
            var city = await _city.FindAsync(cityId);

            return Ok(city);

        }
        /// <summary>
        /// Create a new City
        /// </summary>
        /// <param name="city">Body of new city</param>
        /// <returns>Return new city with 201 status code</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<CityConfirmation>> PostCity([FromBody] CityPostBody city)
        {
            var newCity = await _city.CreateAsync(city);

            return CreatedAtAction(nameof(GetCityById), new { cityId = newCity.Id }, newCity);
        }

        /// <summary>
        /// Update city
        /// </summary>
        /// <param name="cityId">Id of updated city</param>
        /// <param name="city">new city body</param>
        /// <returns>Updated city</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin")]
        [HttpPut("{cityId}")]
        public async Task<ActionResult<CityConfirmation>> PutCity(Guid cityId, [FromBody] CityPutBody city)
        {
            var updatedCity = await _city.UpdateAsync(cityId, city);

            return Ok(updatedCity);
        }

        /// <summary>
        /// Delete city with "cityId" 
        /// </summary>
        /// <param name="cityId">Id of deleted city</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("{cityId}")]
        public async Task<IActionResult> DeleteCity(Guid cityId)
        {
            await _city.DeleteAsync(cityId);

            return NoContent();
        }
    }
}
