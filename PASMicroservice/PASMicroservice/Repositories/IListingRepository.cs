using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PASMicroservice.Entities;

namespace PASMicroservice.Repositories
{
    public interface IListingRepository
    {
        List<Listing> GetListings(string name = null, string categoryId = null, string listingTypeId = null);
        Listing GetListingById(Guid id);
        ListingConfirmation CreateListing(Listing listing);
        ListingConfirmation UpdateListing(Listing listing);
        void DeleteListing(Guid id);
        void Save();
    }
}
