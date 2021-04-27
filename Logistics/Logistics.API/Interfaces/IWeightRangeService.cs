using Logistics.API.Models.WeightRangeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Interfaces
{
    public interface IWeightRangeService
    {
        Task<IReadOnlyCollection<WeightRangeOverview>> BrowseAsync();
        Task<WeightRangeOverview> FindAsync(Guid id);
        Task<WeightRangeOverview> CreateAsync(WeightRangePostBody weightRange);
        Task<WeightRangeOverview> UpdateAsync(Guid id, WeightRangePutBody weightRange);
        Task DeleteAsync(Guid id);
    }
}
