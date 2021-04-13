﻿using Logistics.API.Interfaces;
using Logistics.API.Models;
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<PurchaseResponse>> GetPurchases()
        {
            var purchases = await _purchase.BrowsePurchases();
            if (purchases.Count == 0)
                return NotFound();
            return Ok(purchases);
        }

        /// <summary>
        /// Get Purchase By Id
        /// </summary>
        /// <param name="purchaseId">Id of Pruchase</param>
        /// <returns>Purchase with "purchaseId"</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{purchaseId}")]
        public async Task<ActionResult<PurchaseResponse>> GetPurchaseById(Guid purchaseId)
        {
            var purchase = await _purchase.FindPurchase(purchaseId);
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
        [HttpPost]
        public async Task<ActionResult<PurchaseResponse>> PostPurchase(PurchasePostBody purchase)
        {
            var p = await _purchase.CreatePurchase(purchase);
            if(p!=null)
                return CreatedAtAction(nameof(GetPurchaseById), new { purchaseId = p.Id }, p);
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
        [HttpPut("{purchaseId}")]
        public async Task<ActionResult<PurchaseResponse>> PutPurchase(Guid purchaseId, PurchasePutBody purchase)
        {
            var p = await _purchase.UpdatePurchase(purchaseId, purchase);
            if (p == null)
                return BadRequest();
            return Ok(p);
        }

        /// <summary>
        /// Delete Purchase
        /// </summary>
        /// <param name="purchaseId">Id of deleted purchase</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{purchaseId}")]
        public async Task<IActionResult> DeletePurchase(Guid purchaseId)
        {
            await _purchase.DeletePurchase(purchaseId);
            return NoContent();
        }
    }
}
