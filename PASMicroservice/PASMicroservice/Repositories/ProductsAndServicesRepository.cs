using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PASMicroservice.DBContexts;
using PASMicroservice.Entities;

namespace PASMicroservice.Repositories
{
    public class ProductsAndServicesRepository : IProductsAndServicesRepository
    {
        private readonly PASContext dbContext;
        public ProductsAndServicesRepository(PASContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<ProductsAndServices> GetPAS()
        {
            return this.dbContext.PAS.ToList();
        }

        public ProductsAndServices GetPASById(Guid id)
        {
            return this.dbContext.PAS.Find(id);
        }

        public ProductsAndServicesConfirmation CreatePAS(ProductsAndServices pas)
        {
            this.dbContext.PAS.Add(pas);
            var pasConfirmation = GetPASById(pas.Id);
            Save();

            return new ProductsAndServicesConfirmation
            {
                Id = pasConfirmation.Id,
                Name = pasConfirmation.Name,
                TypeId = pasConfirmation.TypeId,
                CategoryId = pasConfirmation.CategoryId,
                UserId = pasConfirmation.UserId
            };
        }
        public ProductsAndServicesConfirmation UpdatePAS(ProductsAndServices pas)
        {
            var existing = GetPASById(pas.Id);
           
            try
            {
                existing.Id = pas.Id;
                existing.Name = pas.Name;
                existing.Description = pas.Description;
                existing.Price = pas.Price;
                existing.PriceContact = pas.PriceContact;
                existing.PriceDeal = pas.PriceDeal;
                existing.TypeId = pas.TypeId;
                existing.CategoryId = pas.CategoryId;
                existing.UserId = pas.UserId;

                this.dbContext.PAS.Update(existing);
                Save();

                var pasConfirmation = GetPASById(pas.Id);

                return new ProductsAndServicesConfirmation
                {
                    Id = pasConfirmation.Id,
                    Name = pasConfirmation.Name,
                    TypeId = pasConfirmation.TypeId,
                    CategoryId = pasConfirmation.CategoryId,
                    UserId = pasConfirmation.UserId
                };
            } 
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
        public void DeletePAS(Guid id)
        {
            var pas = this.dbContext.PAS.Find(id);
            this.dbContext.PAS.Remove(pas);
            Save();
        }
        public void Save()
        {
            this.dbContext.SaveChanges();
        }
    }
}
