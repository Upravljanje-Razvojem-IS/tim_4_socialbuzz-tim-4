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
    [Route("api/purchase")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchase;

        public PurchaseController(IPurchaseService purchase)
        {
            _purchase = purchase;
        }

        [HttpGet]
        public async Task<ActionResult<PurchaseResponse>> GetPurchases()
        {
            var purchases = await _purchase.BrowsePurchases();
            if (purchases.Count == 0)
                return NoContent();
            return Ok(purchases);
        }

        [HttpGet("{purchaseId}")]
        public async Task<ActionResult<PurchaseResponse>> GetPurchaseById(Guid purchaseId)
        {
            var purchase = await _purchase.FindPurchase(purchaseId);
            if (purchase == null)
                return NoContent();
            return Ok(purchase);
        }

        [HttpPost]
        public async Task<ActionResult<PurchaseResponse>> PostPurchase(PurchasePostBody purchase)
        {
            var p = await _purchase.CreatePurchase(purchase);
            return CreatedAtAction(nameof(GetPurchaseById), new { purchaseId = p.Id }, p);
        }
        
        [HttpPut("{purchaseId}")]
        public async Task<ActionResult<PurchaseResponse>> PutPurchase(Guid purchaseId, PurchasePutBody purchase)
        {
            var p = await _purchase.UpdatePurchase(purchaseId, purchase);
            if (p == null)
                return BadRequest();
            return Ok(p);
        }

        [HttpDelete("{purchaseId}")]
        public async Task<IActionResult> DeletePurchase(Guid purchaseId)
        {
            await _purchase.DeletePurchase(purchaseId);
            return NoContent();
        }
    }
}
