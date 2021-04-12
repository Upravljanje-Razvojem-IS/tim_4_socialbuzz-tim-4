using Logistics.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Interfaces
{
    public interface IWeightRangeService
    {
        Task<IReadOnlyCollection<WeightRangeResponse>> BrowseAsync();
        Task<WeightRangeResponse> FindAsync(Guid id);
        Task<WeightRangeResponse> CreateAsync(WeightRangePostBody weightRange);
        Task<WeightRangeResponse> UpdateAsync(Guid id, WeightRangePutBody weightRange);
        Task DeleteAsync(Guid id);
    }
}
