using Logistics.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Interfaces
{
    public interface IAddressService
    {
        Task<IReadOnlyCollection<AddressResponse>> BrowseAsync(Guid cityId);
        Task<AddressResponse> FindAsync(Guid cityId, Guid addressId);
        Task<AddressResponse> CreateAsync(Guid cityId, AddressPostBody address);
        Task<AddressResponse> UpdateAsync(Guid cityId, Guid addressId, AddressPutBody address);
        Task DeleteAsync(Guid cityId, Guid addressId);
    }
}
