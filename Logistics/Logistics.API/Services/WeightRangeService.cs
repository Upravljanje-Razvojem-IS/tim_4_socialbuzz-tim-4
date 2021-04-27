using AutoMapper;
using AutoMapper.QueryableExtensions;
using Logistics.API.Interfaces;
using Logistics.API.Models.WeightRangeModels;
using Logistics.Core.Entities;
using Logistics.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Services
{
    public class WeightRangeService : IWeightRangeService
    {
        private readonly LogisticsDbContext _context;
        private readonly IMapper _mapper;

        public WeightRangeService(LogisticsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<WeightRangeOverview>> BrowseAsync()
        {
            var weights = await _context.WeightRanges
                .ProjectTo<WeightRangeOverview>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return weights;
        }
        public async Task<WeightRangeOverview> FindAsync(Guid id)
        {
            var weight = await _context.WeightRanges
                .ProjectTo<WeightRangeOverview>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(e => e.Id == id);
            return weight;
        }

        public async Task<WeightRangeOverview> CreateAsync(WeightRangePostBody weightRange)
        {
            WeightRange weight = new WeightRange
            {
                Id = Guid.NewGuid(),
                MinimalWeight = weightRange.MinimalWeight,
                MaximalWeight = weightRange.MaximalWeight,
                PriceCoefficient = weightRange.PriceCoefficient
            };

            await _context.WeightRanges.AddAsync(weight);
            await _context.SaveChangesAsync();

            return await Task.FromResult(_mapper.Map<WeightRangeOverview>(weight));
        }

        public async Task<WeightRangeOverview> UpdateAsync(Guid id, WeightRangePutBody weightRange)
        {
            var weight = await _context.WeightRanges.FirstOrDefaultAsync(e => e.Id == id);

            if (weight == null)
                return null;

            weight.MinimalWeight = weightRange.MinimalWeight;
            weight.MaximalWeight = weightRange.MaximalWeight;
            weight.PriceCoefficient = weightRange.PriceCoefficient;

            await _context.SaveChangesAsync();
            return await Task.FromResult(_mapper.Map<WeightRangeOverview>(weight));
        }

        public async Task DeleteAsync(Guid id)
        {
            var weight = await _context.WeightRanges.FirstOrDefaultAsync(e => e.Id == id);

            if (weight != null)
            {
                _context.WeightRanges.Remove(weight);
                await _context.SaveChangesAsync();
            }
        }
    }
}
