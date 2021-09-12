using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PASMicroservice.DBContexts;
using PASMicroservice.Entities;

namespace PASMicroservice.Repositories
{
    public class ListingTypeRepository : IListingTypeRepository
    {
        private readonly PASContext dbContext;
        public ListingTypeRepository(PASContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<ListingType> GetTypes()
        {
            return this.dbContext.ListingTypes.ToList();
        }

        public ListingType GetTypeById(int id)
        {
            return this.dbContext.ListingTypes.Find(id);
        }

        public ListingTypeConfirmation CreateType(ListingType listingType)
        {
            var entity = this.dbContext.ListingTypes.Add(listingType);
            var listingTypeConfirmation = GetTypeById((int) entity.Property("ListingTypeId").CurrentValue);
            Save();

            return new ListingTypeConfirmation
            {
                ListingTypeId = listingTypeConfirmation.ListingTypeId,
                Name = listingTypeConfirmation.Name
            };
        }

        public ListingTypeConfirmation UpdateType(ListingType listingType)
        {
            var existing = GetTypeById(listingType.ListingTypeId);

            try
            {
                existing.ListingTypeId = listingType.ListingTypeId;
                existing.Name = listingType.Name;

                this.dbContext.ListingTypes.Update(existing);
                Save();

                var listingTypeConfirmation = GetTypeById(listingType.ListingTypeId);
                return new ListingTypeConfirmation
                {
                    ListingTypeId = listingTypeConfirmation.ListingTypeId,
                    Name = listingTypeConfirmation.Name
                };
            } 
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteType(int id)
        {
            var listingType = this.dbContext.ListingTypes.Find(id);
            this.dbContext.ListingTypes.Remove(listingType);
            Save();
        }

        public void Save()
        {
            this.dbContext.SaveChanges();
        }
    }
}
