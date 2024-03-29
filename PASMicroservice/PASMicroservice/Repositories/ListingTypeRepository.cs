﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

            existing.ListingTypeId = listingType.ListingTypeId;
            existing.Name = listingType.Name;

            this.dbContext.ListingTypes.Attach(existing);
            this.dbContext.Entry(existing).State = EntityState.Modified;
            Save();

            return new ListingTypeConfirmation
            {
                ListingTypeId = existing.ListingTypeId,
                Name = existing.Name
            };
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
