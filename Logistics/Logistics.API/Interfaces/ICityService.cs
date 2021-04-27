using Logistics.API.Models.CityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Logistics.API.Interfaces
{
    public interface ICityService
    {
        Task<IReadOnlyCollection<CityOverview>> BrowseAsync();
        Task<CityDetails> FindAsync(Guid id);
        Task<CityConfirmation> CreateAsync(CityPostBody city);
        Task<CityConfirmation> UpdateAsync(Guid id, CityPutBody city);
        Task DeleteAsync(Guid id);
    }
}
