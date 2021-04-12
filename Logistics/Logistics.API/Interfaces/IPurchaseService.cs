using Logistics.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Interfaces
{
    public interface IPurchaseService
    {
        Task<IReadOnlyCollection<PurchaseResponse>> BrowsePurchases();
        Task<PurchaseResponse> FindPurchase(Guid id);
        Task<PurchaseResponse> CreatePurchase(PurchasePostBody purchase);
        Task<PurchaseResponse> UpdatePurchase(Guid id, PurchasePutBody purchase);
        Task DeletePurchase(Guid id);


    }
}
