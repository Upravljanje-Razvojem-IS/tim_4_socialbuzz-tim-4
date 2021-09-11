using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PASMicroservice.DBContexts;
using PASMicroservice.Entities;

namespace PASMicroservice.Repositories
{
    public class ListingRepository : IListingRepository
    {
        private readonly DBContexts.PASContext dbContext;
        public ListingRepository(DBContexts.PASContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Listing> GetListings()
        {
            return this.dbContext.Listings.ToList();
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
           
            try
            {
                existing.ListingId = listing.ListingId;
                existing.Name = listing.Name;
                existing.Description = listing.Description;
                existing.Price = listing.Price;
                existing.PriceContact = listing.PriceContact;
                existing.PriceDeal = listing.PriceDeal;
                existing.CategoryId = listing.CategoryId;
                existing.ListingTypeId = listing.ListingTypeId;
                existing.UserId = listing.UserId;

                this.dbContext.Listings.Update(existing);
                Save();

                var listingConfirmation = GetListingById(listing.ListingId);

                return new ListingConfirmation
                {
                    ListingId = listingConfirmation.ListingId,
                    Name = listingConfirmation.Name,
                    CategoryId = listingConfirmation.CategoryId,
                    ListingTypeId = listingConfirmation.ListingTypeId,
                    UserId = listingConfirmation.UserId
                };
            } 
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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
