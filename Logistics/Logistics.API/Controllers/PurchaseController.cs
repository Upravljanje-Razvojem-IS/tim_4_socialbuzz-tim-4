using Logistics.API.Interfaces;
using Logistics.API.Models.PurchaseModels;
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
    [Route("api/purchase")]
    [Authorize(Roles = "Admin,User")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchase;

        public PurchaseController(IPurchaseService purchase)
        {
            _purchase = purchase;
        }

        /// <summary>
        /// Get all Purchases
        /// </summary>
        /// <returns>List of purchases</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet]
        public async Task<ActionResult<PurchaseOverview>> GetPurchases()
        {
            var purchases = await _purchase.BrowseAsync();
            if (purchases.Count == 0)
                return NoContent();
            return Ok(purchases);
        }

        /// <summary>
        /// Get Purchase By Id
        /// </summary>
        /// <param name="purchaseId">Id of Pruchase</param>
        /// <returns>Purchase with "purchaseId"</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("{purchaseId}")]
        public async Task<ActionResult<PurchaseDetails>> GetPurchaseById(Guid purchaseId)
        {
            var purchase = await _purchase.FindAsync(purchaseId);
            if (purchase == null)
                return NotFound();
            return Ok(purchase);
        }

        /// <summary>
        /// Create new Purchase
        /// </summary>
        /// <param name="purchase">Body of new Purchase</param>
        /// <returns>New Purchase</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPost]
        public async Task<ActionResult<PurchaseConfirmation>> PostPurchase(PurchasePostBody purchase)
        {
            var newPurchase = await _purchase.CreateAsync(purchase);
            if(newPurchase!=null)
                return CreatedAtAction(nameof(GetPurchaseById), new { purchaseId = newPurchase.Id }, newPurchase);
            return BadRequest();
        }

        /// <summary>
        /// Update Purchase
        /// </summary>
        /// <param name="purchaseId">Id of updated Purchase</param>
        /// <param name="purchase">New body of Purchase</param>
        /// <returns>Updated purchase</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("{purchaseId}")]
        public async Task<ActionResult<PurchaseConfirmation>> PutPurchase(Guid purchaseId, PurchasePutBody purchase)
        {
            var updatedPurchase = await _purchase.UpdateAsync(purchaseId, purchase);
            if (updatedPurchase == null)
                return BadRequest();
            return Ok(updatedPurchase);
        }

        /// <summary>
        /// Delete Purchase
        /// </summary>
        /// <param name="purchaseId">Id of deleted purchase</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpDelete("{purchaseId}")]
        public async Task<IActionResult> DeletePurchase(Guid purchaseId)
        {
            await _purchase.RemoveAsync(purchaseId);
            return NoContent();
        }
    }
}
