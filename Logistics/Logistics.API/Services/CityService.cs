using AutoMapper;
using AutoMapper.QueryableExtensions;
using Logistics.API.Interfaces;
using Logistics.API.MockLogger;
using Logistics.API.Models.CityModels;
using Logistics.Core.Entities;
using Logistics.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Services
{
    public class CityService : ICityService
    {
        private readonly LogisticsDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFakeLogger _logger;

        public CityService(LogisticsDbContext context, IMapper mapper, IFakeLogger logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IReadOnlyCollection<CityOverview>> BrowseAsync()
        {
            var cities = await _context.Cities
                .ProjectTo<CityOverview>(_mapper.ConfigurationProvider)
                .ToListAsync();
            _logger.Log("City - BrowseAsync() executed");

            return await Task.FromResult(cities);
        }

        public async Task<CityDetails> FindAsync(Guid id)
        {

            var cityById = await _context.Cities
                .ProjectTo<CityDetails>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(e => e.Id == id);
            _logger.Log("City - FindAsync() executed");

            return await Task.FromResult(cityById);
        }

        public async Task<CityConfirmation> CreateAsync(CityPostBody city)
        {
            City newCity = new City
            {
                Id = Guid.NewGuid(),
                Name = city.Name,
                PostalCode = city.PostalCode,
                Latitude = city.Latitude,
                Longitude = city.Longitude
            };

            await _context.Cities.AddAsync(newCity);
            await _context.SaveChangesAsync();
            _logger.Log("City - CreateAsync() executed");

            return await Task.FromResult(_mapper.Map<CityConfirmation>(newCity));
        }

        public async Task<CityConfirmation> UpdateAsync(Guid id, CityPutBody city)
        {
            var cityToUpdate = await _context.Cities
                .FirstOrDefaultAsync(e => e.Id == id);

            if(cityToUpdate == null)
            {
                _logger.Log("City - UpdateAsync() city with that Id doesn't exist");
                return null;
            }

            cityToUpdate.Name = city.Name;
            cityToUpdate.PostalCode = city.PostalCode;
            cityToUpdate.Latitude = city.Latitude;
            cityToUpdate.Longitude = city.Longitude;

            await _context.SaveChangesAsync();
            _logger.Log("City - UpdateAsync() executed");

            return await Task.FromResult(_mapper.Map<CityConfirmation>(cityToUpdate));
        }

        public async Task DeleteAsync(Guid id)
        {
            var cityToDelete = await _context.Cities.FirstOrDefaultAsync(e => e.Id == id);

            if (cityToDelete != null)
            {
                _context.Cities.Remove(cityToDelete);
                _logger.Log("City - DeleteAsync() executed");
                await _context.SaveChangesAsync();
            }
            _logger.Log("City - DeleteAsync() City with that id doesn't exist");
        }
    }
}
