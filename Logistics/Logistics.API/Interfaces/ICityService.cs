using Logistics.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Logistics.API.Interfaces
{
    public interface ICityService
    {
        Task<IReadOnlyCollection<CityResponseBody>> BrowseAsync();
        Task<CityResponseBody> FindAsync(Guid id);
        Task<CityResponseBody> CreateAsync(CityPostBody city);
        Task<CityResponseBody> UpdateAsync(Guid id, CityPutBody city);
        Task DeleteAsync(Guid id);
    }
}
