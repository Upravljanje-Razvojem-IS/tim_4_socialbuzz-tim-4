using Logistics.API.Interfaces;
using Logistics.API.Models.AddressModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Logistics.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/addresses")]
    [Authorize(Roles = "Admin,User")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _address;

        public AddressController(IAddressService address)
        {
            _address = address;
        }

        /// <summary>
        /// Get all addresses with "cityId" param
        /// </summary>
        /// <param name="cityId">Id of city where address is</param>
        /// <returns>Return list of all addresses in city with "cityId" id</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet]
        public async Task<ActionResult<AddressOverview>> GetAddresses(Guid cityId)
        {
            var addresses = await _address.BrowseAsync(cityId);

            if (addresses.Count == 0)
            {
                return NoContent();
            }

            return Ok(addresses);
        }

        /// <summary>
        /// Get address with "cityId" and "addressId" param
        /// </summary>
        /// <param name="cityId">Id of city where address is</param>
        /// <param name="addressId">Id of returned address</param>
        /// <returns>Address with "addressId"</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("{addressId}")]
        public async Task<ActionResult<AddressDetails>> GetAddressById(Guid cityId, Guid addressId)
        {
            var address = await _address.FindAsync(cityId, addressId);

            if (address == null)
            {
                return NotFound();
            }
            
            return Ok(address);
        }
        /// <summary>
        /// Create new Address in City with "cityId" Id
        /// </summary>
        /// <param name="cityId">Id of city</param>
        /// <param name="address">New Address body</param>
        /// <returns>New Address with 201 status code</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost]
        public async Task<ActionResult<AddressConfirmation>> PostAddress(Guid cityId, [FromBody] AddressPostBody address)
        {
            var newAddress = await _address.CreateAsync(cityId, address);

            return CreatedAtAction(nameof(GetAddressById), new { cityId, addressId = newAddress.Id }, newAddress);
        }

        /// <summary>
        /// Update address with "addressId" Id
        /// </summary>
        /// <param name="cityId">Id of city where address is</param>
        /// <param name="addressId">Id of updated address</param>
        /// <param name="address">New body to address</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("{addressId}")]
        public async Task<ActionResult<AddressConfirmation>> PutAddress(Guid cityId, Guid addressId, [FromBody] AddressPutBody address)
        {
            var updatedAddress = await _address.UpdateAsync(cityId, addressId, address);

            if (updatedAddress == null)
            {
                return BadRequest("Address with that Id doesnt exist");
            }

            return Ok(updatedAddress);
        }

        /// <summary>
        /// Delete address with "addressId" id
        /// </summary>
        /// <param name="cityId">Id of city where address is</param>
        /// <param name="addressId">Id of deleted address</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpDelete("{addressId}")]
        public async Task<IActionResult> DeleteAddress(Guid cityId, Guid addressId)
        {
            await _address.DeleteAsync(cityId, addressId);
            return NoContent();
        }

    }
}
