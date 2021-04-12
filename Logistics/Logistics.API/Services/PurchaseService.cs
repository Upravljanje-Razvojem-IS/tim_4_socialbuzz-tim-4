using AutoMapper;
using AutoMapper.QueryableExtensions;
using Logistics.API.Interfaces;
using Logistics.API.Models;
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

        public PurchaseService(LogisticsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<PurchaseResponse>> BrowsePurchases()
        {
            var purchases = await _context.Purchases
                .ProjectTo<PurchaseResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return await Task.FromResult(purchases);
        }

        public async Task<PurchaseResponse> FindPurchase(Guid id)
        {
            var purchase = await _context.Purchases
                .ProjectTo<PurchaseResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(e => e.Id == id);
            return await Task.FromResult(purchase);
        }

        public async Task<PurchaseResponse> CreatePurchase(PurchasePostBody purchase)
        {
            var fromAddress = await _context.Addresses.FirstOrDefaultAsync(e => e.Id == purchase.FromAddressId);
            var toAddress = await _context.Addresses.FirstOrDefaultAsync(e => e.Id == purchase.ToAddressId);
            var item = ItemMockService.ItemMocks.FirstOrDefault(e => e.Id == purchase.ItemId);

            if (fromAddress == null || toAddress == null || item == null)
                return null;

            Purchase p = new Purchase
            {
                Id = Guid.NewGuid(),
                ItemId = item.Id,
                ItemMock = item,
                Pieces = purchase.Pieces,
                TotalWeight = 0,
                TotalPriceWithWeightAndDistance = 0,
                FromAddressId = purchase.FromAddressId,
                FromAddess = fromAddress,
                ToAddressId = purchase.ToAddressId,
                ToAddress = toAddress
            };
            p.TotalWeight = p.ItemMock.WeightOfOne * p.Pieces;
            p.WeightRange = await _context.WeightRanges.FirstOrDefaultAsync(e => e.MinimalWeight <= p.TotalWeight && e.MaximalWeight > p.TotalWeight);
            p.WeightRangeId = p.WeightRange.Id;

            p.Distance = CalculateDistance.Calculate(p.FromAddess.City, p.ToAddress.City);
            p.DistancePrice = await _context.DistancePrices.FirstOrDefaultAsync(e => e.MinimalDistance <= p.Distance && e.MaximalDistance > p.Distance);
            p.DistancePriceId = p.DistancePrice.Id;

            p.TotalPriceWithWeightAndDistance = CalculatePrice.Calculate(p.Pieces, item.PriceOfOne, p.DistancePrice.Price, p.WeightRange.PriceCoefficient);

            await _context.Purchases.AddAsync(p);
            await _context.SaveChangesAsync();

            return await Task.FromResult(_mapper.Map<PurchaseResponse>(p));
        }

        public async Task<PurchaseResponse> UpdatePurchase(Guid id, PurchasePutBody purchase)
        {
            var p = await _context.Purchases.FirstOrDefaultAsync(e => e.Id == id);
            if (p == null)
                return null;

            if (p.ItemId != purchase.ItemId)
            {
                var item = ItemMockService.ItemMocks.FirstOrDefault(e => e.Id == purchase.ItemId);
                if (item == null)
                    return null;

                p.ItemMock = item;
                p.ItemId = purchase.ItemId;
            }

            if (p.FromAddressId != purchase.FromAddressId)
            {
                var fromAddress = await _context.Addresses.FirstOrDefaultAsync(e => e.Id == purchase.FromAddressId);
                if (fromAddress == null)
                    return null;

                p.FromAddess = fromAddress;
                p.FromAddressId = purchase.FromAddressId;
            }

            if (p.ToAddressId != purchase.ToAddressId)
            {
                var toAddress = await _context.Addresses.FirstOrDefaultAsync(e => e.Id == purchase.ToAddressId);
                if (toAddress == null)
                    return null;

                p.ToAddress = toAddress;
                p.ToAddressId = purchase.ToAddressId;
            }

            p.Pieces = purchase.Pieces;
            p.TotalWeight = p.ItemMock.WeightOfOne * p.Pieces;
            //total price update bussiness logic method

            await _context.SaveChangesAsync();

            return await Task.FromResult(_mapper.Map<PurchaseResponse>(p));
        }

        public async Task DeletePurchase(Guid id)
        {
            var purchase = await _context.Purchases.FirstOrDefaultAsync(e => e.Id == id);
            if (purchase != null)
            {
                _context.Purchases.Remove(purchase);
                await _context.SaveChangesAsync();
            }

        }
    }
}
