using Logistics.API.Models.WeightRangeModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.API.Interfaces
{
    public interface IWeightRangeService
    {
        Task<IReadOnlyCollection<WeightRangeOverview>> BrowseAsync();
        Task<WeightRangeDetails> FindAsync(Guid id);
        Task<WeightRangeConfirmation> CreateAsync(WeightRangePostBody weightRange);
        Task<WeightRangeConfirmation> UpdateAsync(Guid id, WeightRangePutBody weightRange);
        Task DeleteAsync(Guid id);
    }
}
