using AutoMapper;
using AutoMapper.QueryableExtensions;
using Logistics.API.CustomException;
using Logistics.API.Interfaces;
using Logistics.API.MockLogger;
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
        private readonly IFakeLogger _logger;

        public AddressService(LogisticsDbContext context, IMapper mapper, IFakeLogger logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IReadOnlyCollection<AddressOverview>> BrowseAsync(Guid cityId)
        {
            var addresses = await _context.Addresses
                .ProjectTo<AddressOverview>(_mapper.ConfigurationProvider)
                .Where(e => e.CityId == cityId)
                .ToListAsync();
            _logger.Log("Address - BrowseAsync() executed");

            return await Task.FromResult(addresses);
        }

        public async Task<AddressDetails> FindAsync(Guid cityId, Guid addressId)
        {
            var addressById = await _context.Addresses
                .ProjectTo<AddressDetails>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(e => e.Id == addressId);
            _logger.Log("Address - FindAsync() executed");

            return await Task.FromResult(addressById);
        }

        public async Task<AddressConfirmation> CreateAsync(Guid cityId, AddressPostBody address)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(e => e.Id == cityId);

            if (city == null)
                throw new LogisticException("City doesnt exist", 400);

            Address newAddress = new()
            {
                Id = Guid.NewGuid(),
                Street = address.Street,
                Number = address.Number,
                CityId = cityId
            };
            await _context.Addresses.AddAsync(newAddress);
            await _context.SaveChangesAsync();
            _logger.Log("Address - CreateAsync() executed");

            return await Task.FromResult(_mapper.Map<AddressConfirmation>(newAddress));
        }

        public async Task<AddressConfirmation> UpdateAsync(Guid cityId, Guid addressId, AddressPutBody address)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(e => e.Id == cityId);

            if (city == null)
                throw new LogisticException("City doesnt exist", 400);

            var updatedAddress = await _context.Addresses.FirstOrDefaultAsync(e => e.Id == addressId);

            if (updatedAddress == null)
                throw new LogisticException("Address doesnt exist", 400);

            updatedAddress.Street = address.Street;
            updatedAddress.Number = address.Number;
            await _context.SaveChangesAsync();
            _logger.Log("Address - UpdateAsync() executed");

            return await Task.FromResult(_mapper.Map<AddressConfirmation>(updatedAddress));
        }

        public async Task DeleteAsync(Guid cityId, Guid addressId)
        {
            var addressToDelete = await _context.Addresses.FirstOrDefaultAsync(e => e.Id == addressId);

            if (addressToDelete == null)
                throw new LogisticException("Address doesnt exists", 400);

            _context.Remove(addressToDelete);
            await _context.SaveChangesAsync();
            _logger.Log("Address - DeleteAsync() executed");
        }


    }
}
