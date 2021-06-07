using AutoMapper;
using AutoMapper.QueryableExtensions;
using Logistics.API.CustomException;
using Logistics.API.Interfaces;
using Logistics.API.MockLogger;
using Logistics.API.Models.PurchaseModels;
using Logistics.Core.BusinessLogic;
using Logistics.Core.Entities;
using Logistics.Core.Mock;
using Logistics.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.API.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly LogisticsDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFakeLogger _logger;

        public PurchaseService(LogisticsDbContext context, IMapper mapper, IFakeLogger logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IReadOnlyCollection<PurchaseOverview>> BrowseAsync()
        {
            var purchases = await _context.Purchases
                .ProjectTo<PurchaseOverview>(_mapper.ConfigurationProvider)
                .ToListAsync();
            _logger.Log("Purchase BrowseAsync() executed!");
            return await Task.FromResult(purchases);
        }

        public async Task<PurchaseDetails> FindAsync(Guid id)
        {
            var purchaseById = await _context.Purchases
                .ProjectTo<PurchaseDetails>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(e => e.Id == id);
            _logger.Log("Purchase BrowseAsync() executed!");

            if (purchaseById == null)
                throw new LogisticException("Purchase not found", 404);

            return await Task.FromResult(purchaseById);
        }

        public async Task<PurchaseConfirmation> CreateAsync(PurchasePostBody purchase)
        {
            var fromAddress = await _context.Addresses.FirstOrDefaultAsync(e => e.Id == purchase.FromAddressId);
            fromAddress.City = await _context.Cities.FirstOrDefaultAsync(e => e.Id == fromAddress.CityId);
            var toAddress = await _context.Addresses.FirstOrDefaultAsync(e => e.Id == purchase.ToAddressId);
            toAddress.City = await _context.Cities.FirstOrDefaultAsync(e => e.Id == toAddress.CityId);

            var item = ItemMockService.ItemMocks.FirstOrDefault(e => e.Id == purchase.ItemId);


            Purchase newPurchase = new()
            {
                Id = Guid.NewGuid(),
                ItemId = item.Id,
                Pieces = purchase.Pieces,
                TotalWeight = 0,
                TotalPriceWithWeightAndDistance = 0,
                FromAddressId = purchase.FromAddressId,
                ToAddressId = purchase.ToAddressId
            };

            newPurchase.TotalWeight = item.WeightOfOne * newPurchase.Pieces;
            newPurchase.WeightRangeId = newPurchase.WeightRange.Id;
            newPurchase.Distance = CalculateDistance.Calculate(fromAddress.City, toAddress.City);
            newPurchase.DistancePriceId = newPurchase.DistancePrice.Id;
            newPurchase.TotalPriceWithWeightAndDistance = CalculatePrice.Calculate(newPurchase.Pieces, item.PriceOfOne, newPurchase.DistancePrice.Price, newPurchase.WeightRange.PriceCoefficient);

            await _context.Purchases.AddAsync(newPurchase);
            await _context.SaveChangesAsync();
            _logger.Log("Purchase CreateAsync() executed!");

            return await Task.FromResult(_mapper.Map<PurchaseConfirmation>(newPurchase));
        }

        public async Task<PurchaseConfirmation> UpdateAsync(Guid id, PurchasePutBody purchase)
        {
            var p = await _context.Purchases.FirstOrDefaultAsync(e => e.Id == id);

            if (p == null)
                throw new LogisticException("Purchase doesnt exist", 400);

            p.ItemId = purchase.ItemId;

            var fromAddress = await _context.Addresses.FirstOrDefaultAsync(e => e.Id == purchase.FromAddressId);
            p.FromAddess = fromAddress;
            p.FromAddressId = purchase.FromAddressId;

            var toAddress = await _context.Addresses.FirstOrDefaultAsync(e => e.Id == purchase.ToAddressId);
            p.ToAddress = toAddress;
            p.ToAddressId = purchase.ToAddressId;
            p.Pieces = purchase.Pieces;

            if (p.Pieces == 0)
                throw new LogisticException("Pieces cant be 0", 400);

            var item = ItemMockService.ItemMocks.FirstOrDefault(e => e.Id == purchase.ItemId);
            p.TotalWeight = item.WeightOfOne * p.Pieces;
            var weightRange = await _context.WeightRanges.FirstOrDefaultAsync(e => e.MinimalWeight < p.TotalWeight && e.MaximalWeight >= p.TotalWeight);
            p.WeightRangeId = weightRange.Id;

            var fromCity = await _context.Cities.FirstOrDefaultAsync(e => e.Id == p.FromAddess.CityId);
            var toCity = await _context.Cities.FirstOrDefaultAsync(e => e.Id == p.ToAddress.CityId);
            p.Distance = CalculateDistance.Calculate(fromCity, toCity);
            var distancePrice = await _context.DistancePrices.FirstOrDefaultAsync(e => e.MinimalDistance <= p.Distance && e.MaximalDistance > p.Distance);
            p.DistancePriceId = distancePrice.Id;
            p.TotalPriceWithWeightAndDistance = CalculatePrice.Calculate(p.Pieces, item.PriceOfOne, distancePrice.Price, weightRange.PriceCoefficient);

            await _context.SaveChangesAsync();
            _logger.Log("Purchase UpdateAsync() executed!");

            return await Task.FromResult(_mapper.Map<PurchaseConfirmation>(p));
        }

        public async Task RemoveAsync(Guid id)
        {
            var purchase = await _context.Purchases.FirstOrDefaultAsync(e => e.Id == id);

            if (purchase == null)
                throw new LogisticException("Purchase doesnt exist", 400);

            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();
            _logger.Log("Purchase DeleteAsync() executed!");

        }
    }
}
