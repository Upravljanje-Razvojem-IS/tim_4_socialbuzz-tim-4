using Logistics.API.Models.AddressModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Interfaces
{
    public interface IAddressService
    {
        Task<IReadOnlyCollection<AddressOverview>> BrowseAsync(Guid cityId);
        Task<AddressOverview> FindAsync(Guid cityId, Guid addressId);
        Task<AddressOverview> CreateAsync(Guid cityId, AddressPostBody address);
        Task<AddressOverview> UpdateAsync(Guid cityId, Guid addressId, AddressPutBody address);
        Task DeleteAsync(Guid cityId, Guid addressId);
    }
}
