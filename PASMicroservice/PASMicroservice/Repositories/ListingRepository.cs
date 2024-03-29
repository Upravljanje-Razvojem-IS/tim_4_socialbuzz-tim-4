﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PASMicroservice.DBContexts;
using PASMicroservice.Entities;

namespace PASMicroservice.Repositories
{
    public class ListingRepository : IListingRepository
    {
        private readonly PASContext dbContext;
        public ListingRepository(PASContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<Listing> GetListings(string name = null, string categoryId = null, string listingTypeId = null)
        {
            return this.dbContext.Listings.Where(e =>
                (name == null || e.Name.Contains(name)) &&
                (categoryId == null || e.CategoryId == Guid.Parse(categoryId)) &&
                (listingTypeId == null || e.ListingTypeId == Convert.ToInt32(listingTypeId)))
            .ToList();
        }

        public Listing GetListingById(Guid id)
        {
            return this.dbContext.Listings.Find(id);
        }

        public ListingConfirmation CreateListing(Listing listing)
        {
            this.dbContext.Listings.Add(listing);
            var listingConfirmation = GetListingById(listing.ListingId);
            Save();

            return new ListingConfirmation
            {
                ListingId = listingConfirmation.ListingId,
                Name = listingConfirmation.Name,
                CategoryId = listingConfirmation.CategoryId,
                ListingTypeId = listingConfirmation.ListingTypeId,
                UserId = listingConfirmation.UserId
            };
        }
        public ListingConfirmation UpdateListing(Listing listing)
        {
            var existing = GetListingById(listing.ListingId);

            existing.ListingId = listing.ListingId;
            existing.Name = listing.Name;
            existing.Description = listing.Description;
            existing.Price = listing.Price;
            existing.PriceContact = listing.PriceContact;
            existing.PriceDeal = listing.PriceDeal;
            existing.CategoryId = listing.CategoryId;
            existing.ListingTypeId = listing.ListingTypeId;
            existing.UserId = listing.UserId;

            this.dbContext.Listings.Attach(existing);
            this.dbContext.Entry(existing).State = EntityState.Modified;
            Save();

            return new ListingConfirmation
            {
                ListingId = existing.ListingId,
                Name = existing.Name,
                CategoryId = existing.CategoryId,
                ListingTypeId = existing.ListingTypeId,
                UserId = existing.UserId
            };
        }
        public void DeleteListing(Guid id)
        {
            var listing = this.dbContext.Listings.Find(id);
            this.dbContext.Listings.Remove(listing);
            Save();
        }
        public void Save()
        {
            this.dbContext.SaveChanges();
        }
    }
}
