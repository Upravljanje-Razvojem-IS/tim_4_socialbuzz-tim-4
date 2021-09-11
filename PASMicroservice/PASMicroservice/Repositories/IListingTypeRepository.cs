using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PASMicroservice.Entities;

namespace PASMicroservice.Repositories
{
    public interface IListingTypeRepository
    {
        List<ListingType> GetTypes();
        ListingType GetTypeById(int id);
        ListingTypeConfirmation CreateType(ListingType listingType);
        ListingTypeConfirmation UpdateType(ListingType listingType);
        void DeleteType(int id);
        void Save();
    }
}
