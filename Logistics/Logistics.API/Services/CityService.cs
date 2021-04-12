using AutoMapper;
using AutoMapper.QueryableExtensions;
using Logistics.API.Interfaces;
using Logistics.API.Models;
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

        public CityService(LogisticsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<CityResponseBody>> BrowseAsync()
        {
            var cities = await _context.Cities
                .ProjectTo<CityResponseBody>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return await Task.FromResult(cities);
        }

        public async Task<CityResponseBody> FindAsync(Guid id)
        {

            var city = await _context.Cities
                .ProjectTo<CityResponseBody>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(e => e.Id == id);

            return await Task.FromResult(city);
        }

        public async Task<CityResponseBody> CreateAsync(CityPostBody city)
        {
            City c = new City
            {
                Id = Guid.NewGuid(),
                Name = city.Name,
                PostalCode = city.PostalCode,
                Latitude = city.Latitude,
                Longitude = city.Longitude
            };

            await _context.Cities.AddAsync(c);
            await _context.SaveChangesAsync();

            return await Task.FromResult(_mapper.Map<CityResponseBody>(c));
        }

        public async Task<CityResponseBody> UpdateAsync(Guid id, CityPutBody city)
        {
            var c = await _context.Cities
                .FirstOrDefaultAsync(e => e.Id == id);

            if(c == null)
                return null;

            c.Name = city.Name;
            c.PostalCode = city.PostalCode;
            c.Latitude = city.Latitude;
            c.Longitude = city.Longitude;

            await _context.SaveChangesAsync();

            return await Task.FromResult(_mapper.Map<CityResponseBody>(c));
        }

        public async Task DeleteAsync(Guid id)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(e => e.Id == id);

            if (city != null)
            {
                _context.Cities.Remove(city);
                await _context.SaveChangesAsync();
            }
           
        }
    }
}
