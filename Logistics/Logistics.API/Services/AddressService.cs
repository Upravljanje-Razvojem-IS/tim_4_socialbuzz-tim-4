using AutoMapper;
using AutoMapper.QueryableExtensions;
using Logistics.API.Interfaces;
using Logistics.API.Models.AddressModels;
using Logistics.Core.Entities;
using Logistics.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Services
{
    public class AddressService : IAddressService
    {
        private readonly LogisticsDbContext _context;
        private readonly IMapper _mapper;

        public AddressService(LogisticsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<AddressOverview>> BrowseAsync(Guid cityId)
        {
            var addresses = await _context.Addresses
                .ProjectTo<AddressOverview>(_mapper.ConfigurationProvider)
                .Where(e => e.CityId == cityId)
                .ToListAsync();
            return await Task.FromResult(addresses);
        }

        public async Task<AddressOverview> FindAsync(Guid cityId, Guid addressId)
        {
            var address = await _context.Addresses
                .ProjectTo<AddressOverview>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(e => e.Id == addressId);
            return await Task.FromResult(address);
        }

        public async Task<AddressOverview> CreateAsync(Guid cityId, AddressPostBody address)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(e => e.Id == cityId);
            
            if(city == null)
                return null;

            Address a = new Address
            {
                Id = Guid.NewGuid(),
                Street = address.Street,
                Number = address.Number,
                CityId = cityId,
                City = city
            };

            await _context.Addresses.AddAsync(a);
            await _context.SaveChangesAsync();

            return await Task.FromResult(_mapper.Map<AddressOverview>(a));
        }

        public async Task<AddressOverview> UpdateAsync(Guid cityId, Guid addressId, AddressPutBody address)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(e => e.Id == cityId);

            if (city == null)
                return null;

            var a = await _context.Addresses.FirstOrDefaultAsync(e => e.Id == addressId);

            if (a == null)
                return null;

            a.Street = address.Street;
            a.Number = address.Number;

            await _context.SaveChangesAsync();

            return await Task.FromResult(_mapper.Map<AddressOverview>(a));
        }

        public async Task DeleteAsync(Guid cityId, Guid addressId)
        {
            var address = await _context.Addresses.FirstOrDefaultAsync(e => e.Id == addressId);

            if (address != null)
            {
                _context.Remove(address);
                await _context.SaveChangesAsync();
            }
        }


    }
}
