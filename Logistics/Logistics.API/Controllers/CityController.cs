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
    [Route("api/cities")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _city;

        public CityController(ICityService city)
        {
            _city = city;
        }

        [HttpGet]
        public async Task<ActionResult<CityResponseBody>> GetCities()
        {
            var cities = await _city.BrowseAsync();
            if (cities.Count == 0)
                return NoContent();
            return Ok(cities);
        }

        [HttpGet("{cityId}")]
        public async Task<ActionResult<CityResponseBody>> GetCityById(Guid cityId)
        {
            var city = await _city.FindAsync(cityId);
            if (city == null)
                return NoContent();
            return Ok(city);

        }

        [HttpPost]
        public async Task<ActionResult<CityResponseBody>> PostCity([FromBody] CityPostBody city)
        {
            var c = await _city.CreateAsync(city);
            return CreatedAtAction(nameof(GetCityById), new { cityId = c.Id }, city);
        }

        [HttpPut("{cityId}")]
        public async Task<ActionResult<CityResponseBody>> PutCity(Guid cityId, [FromBody] CityPutBody city)
        {
            var c = await _city.UpdateAsync(cityId, city);
            if(c != null)
                return Ok(c);

            return BadRequest("City with that ID doesn't exist");
        }

        [HttpDelete("{cityId}")]
        public async Task<IActionResult> DeleteCity(Guid cityId)
        {
            await _city.DeleteAsync(cityId);
            return NoContent();
        }
    }
}
