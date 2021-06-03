using Logistics.API.Models.PurchaseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.API.Interfaces
{
    public interface IPurchaseService
    {
        Task<IReadOnlyCollection<PurchaseOverview>> BrowseAsync();
        Task<PurchaseDetails> FindAsync(Guid id);
        Task<PurchaseConfirmation> CreateAsync(PurchasePostBody purchase);
        Task<PurchaseConfirmation> UpdateAsync(Guid id, PurchasePutBody purchase);
        Task RemoveAsync(Guid id);


    }
}
