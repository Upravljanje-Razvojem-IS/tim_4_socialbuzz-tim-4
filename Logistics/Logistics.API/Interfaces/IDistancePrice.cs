using Logistics.API.Models.DistancePriceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Interfaces
{
    public interface IDistancePrice
    {
        Task<IReadOnlyCollection<DistancePriceOverview>> BrowseAsync();
        Task<DistancePriceDetails> FindAsync(Guid id);
        Task<DistancePriceConfirmation> CreateAsync(DistancePricePostBody distancePrice);
        Task<DistancePriceConfirmation> UpdateAsync(Guid id, DistancePricePutBody distancePrice);
        Task DeleteAsync(Guid id);
    }
}
