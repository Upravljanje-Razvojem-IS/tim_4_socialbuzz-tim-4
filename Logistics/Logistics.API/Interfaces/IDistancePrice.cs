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
        Task<DistancePriceOverview> FindAsync(Guid id);
        Task<DistancePriceOverview> CreateAsync(DistancePricePostBody distancePrice);
        Task<DistancePriceOverview> UpdateAsync(Guid id, DistancePricePutBody distancePrice);
        Task DeleteAsync(Guid id);
    }
}
