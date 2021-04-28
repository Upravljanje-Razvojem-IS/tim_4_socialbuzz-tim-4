using AutoMapper;
using AutoMapper.QueryableExtensions;
using Logistics.API.Interfaces;
using Logistics.API.MockLogger;
using Logistics.API.Models.DistancePriceModels;
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
        private readonly IFakeLogger _logger;

        public DistancePriceService(LogisticsDbContext context, IMapper mapper, IFakeLogger logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IReadOnlyCollection<DistancePriceOverview>> BrowseAsync()
        {
            var distances = await _context.DistancePrices
                .ProjectTo<DistancePriceOverview>(_mapper.ConfigurationProvider)
                .ToListAsync();
            _logger.Log("DistancePrice BrowseAsync() executed!");
            return await Task.FromResult(distances);
        }

        public async Task<DistancePriceDetails> FindAsync(Guid id)
        {
            var distanceById = await _context.DistancePrices
                .ProjectTo<DistancePriceDetails>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(e => e.Id == id);
            _logger.Log("DistancePrice FindAsync() executed!");
            return await Task.FromResult(distanceById);
        }

        public async Task<DistancePriceConfirmation> CreateAsync(DistancePricePostBody distancePrice)
        {
            DistancePrice newDistance = new()
            {
                Id = Guid.NewGuid(),
                MinimalDistance = distancePrice.MinimalDistance,
                MaximalDistance = distancePrice.MaximalDistance,
                Price = distancePrice.Price
            };
            await _context.DistancePrices.AddAsync(newDistance);
            await _context.SaveChangesAsync();
            _logger.Log("DistancePrice CreateAsync() executed!");

            return await Task.FromResult(_mapper.Map<DistancePriceConfirmation>(newDistance));
        }

        public async Task<DistancePriceConfirmation> UpdateAsync(Guid id, DistancePricePutBody distancePrice)
        {
            var updateDistane = await _context.DistancePrices.FirstOrDefaultAsync(e => e.Id == id);
            if (updateDistane == null)
            {
                _logger.Log("DistancePrice UpdateAsync() DistancePrice doesn't exist with given Id");
                return null;
            }
            updateDistane.MinimalDistance = distancePrice.MinimalDistance;
            updateDistane.MaximalDistance = distancePrice.MaximalDistance;
            updateDistane.Price = distancePrice.Price;
            await _context.SaveChangesAsync();
            _logger.Log("DistancePrice UpdateAsync() executed!");

            return await Task.FromResult(_mapper.Map<DistancePriceConfirmation>(updateDistane));
        }

        public async Task DeleteAsync(Guid id)
        {
            var deleteDistance = await _context.DistancePrices
                .FirstOrDefaultAsync(e => e.Id == id);
            if (deleteDistance != null)
            {
                _context.DistancePrices.Remove(deleteDistance);
                await _context.SaveChangesAsync();
                _logger.Log("DistancePrice DeleteAsync() executed!");
            }
            _logger.Log("DistancePrice with given Id doesn't exist");
        }
    }
}
