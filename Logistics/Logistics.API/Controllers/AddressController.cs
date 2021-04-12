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
    [Route("api/cities/{cityId}/addresses")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _address;

        public AddressController(IAddressService address)
        {
            _address = address;
        }

        [HttpGet]
        public async Task<ActionResult<AddressResponse>> GetAddresses(Guid cityId)
        {
            var addresses = await _address.BrowseAsync(cityId);

            if (addresses.Count == 0)
                return NoContent();

            return Ok(addresses);
        }

        [HttpGet("{addressId}")]
        public async Task<ActionResult<AddressResponse>> GetAddressById(Guid cityId, Guid addressId)
        {
            var address = await _address.FindAsync(cityId, addressId);

            if (address == null)
                return NoContent();

            return Ok(address);
        }

        [HttpPost]
        public async Task<ActionResult<AddressResponse>> PostAddress(Guid cityId, [FromBody] AddressPostBody address)
        {
            var a = await _address.CreateAsync(cityId, address);
            return CreatedAtAction(nameof(GetAddressById), new { cityId, addressId = a.Id }, a);
        }

        [HttpPut("{addressId}")]
        public async Task<ActionResult<AddressResponse>> PutAddress(Guid cityId, Guid addressId, [FromBody] AddressPutBody address)
        {
            var a = await _address.UpdateAsync(cityId, addressId, address);

            if (a == null)
                return BadRequest("Address with that Id doesnt exist");
            return Ok(a);
        }

        [HttpDelete("{addressId}")]
        public async Task<IActionResult> DeleteAddress(Guid cityId, Guid addressId)
        {
            await _address.DeleteAsync(cityId, addressId);
            return NoContent();
        }

    }
}
