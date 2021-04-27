using Logistics.API.Models.PurchaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Interfaces
{
    public interface IPurchaseService
    {
        Task<IReadOnlyCollection<PurchaseOverview>> BrowsePurchases();
        Task<PurchaseDetails> FindPurchase(Guid id);
        Task<PurchaseConfirmation> CreatePurchase(PurchasePostBody purchase);
        Task<PurchaseConfirmation> UpdatePurchase(Guid id, PurchasePutBody purchase);
        Task DeletePurchase(Guid id);


    }
}
