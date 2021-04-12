using Logistics.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Interfaces
{
    public interface IDistancePrice
    {
        Task<IReadOnlyCollection<DistancePriceResponse>> BrowseAsync();
        Task<DistancePriceResponse> FindAsync(Guid id);
        Task<DistancePriceResponse> CreateAsync(DistancePricePostBody distancePrice);
        Task<DistancePriceResponse> UpdateAsync(Guid id, DistancePricePutBody distancePrice);
        Task DeleteAsync(Guid id);
    }
}
