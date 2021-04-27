using Logistics.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Interfaces
{
    public interface IPurchaseService
    {
        Task<IReadOnlyCollection<PurchaseOverview>> BrowsePurchases();
        Task<PurchaseOverview> FindPurchase(Guid id);
        Task<PurchaseOverview> CreatePurchase(PurchasePostBody purchase);
        Task<PurchaseOverview> UpdatePurchase(Guid id, PurchasePutBody purchase);
        Task DeletePurchase(Guid id);


    }
}
