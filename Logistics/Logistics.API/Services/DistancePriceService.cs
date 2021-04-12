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
    public class DistancePriceService : IDistancePrice
    {
        private readonly LogisticsDbContext _context;
        private readonly IMapper _mapper;

        public DistancePriceService(LogisticsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<DistancePriceResponse>> BrowseAsync()
        {
            var distances = await _context.DistancePrices
                .ProjectTo<DistancePriceResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return await Task.FromResult(distances);
        }

        public async Task<DistancePriceResponse> FindAsync(Guid id)
        {
            var distance = await _context.DistancePrices
                .ProjectTo<DistancePriceResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(e => e.Id == id);

            return await Task.FromResult(distance);
        }

        public async Task<DistancePriceResponse> CreateAsync(DistancePricePostBody distancePrice)
        {
            DistancePrice distance = new DistancePrice
            {
                Id = Guid.NewGuid(),
                MinimalDistance = distancePrice.MinimalDistance,
                MaximalDistance = distancePrice.MaximalDistance,
                Price = distancePrice.Price
            };

            await _context.DistancePrices.AddAsync(distance);
            await _context.SaveChangesAsync();

            return await Task.FromResult(_mapper.Map<DistancePriceResponse>(distance));
        }

        public async Task<DistancePriceResponse> UpdateAsync(Guid id, DistancePricePutBody distancePrice)
        {
            var distance = await _context.DistancePrices.FirstOrDefaultAsync(e => e.Id == id);

            if (distance == null)
                return null;

            distance.MinimalDistance = distancePrice.MinimalDistance;
            distance.MaximalDistance = distancePrice.MaximalDistance;
            distance.Price = distancePrice.Price;

            await _context.SaveChangesAsync();

            return await Task.FromResult(_mapper.Map<DistancePriceResponse>(distance));
        }

        public async Task DeleteAsync(Guid id)
        {
            var distance = await _context.DistancePrices
                .FirstOrDefaultAsync(e => e.Id == id);
            if (distance != null)
            {
                _context.DistancePrices.Remove(distance);
                await _context.SaveChangesAsync();
            }
        }
    }
}
