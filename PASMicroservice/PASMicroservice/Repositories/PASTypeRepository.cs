using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PASMicroservice.DBContexts;
using PASMicroservice.Entities;

namespace PASMicroservice.Repositories
{
    public class PASTypeRepository : IPASTypeRepository
    {
        private readonly PASContext dbContext;
        public PASTypeRepository(PASContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<PASType> GetTypes()
        {
            return this.dbContext.PASTypes.ToList();
        }

        public PASType GetTypeById(int id)
        {
            return this.dbContext.PASTypes.Find(id);
        }

        public PASTypeConfirmation CreateType(PASType type)
        {
            var entity = this.dbContext.PASTypes.Add(type);
            var typeConfirmation = GetTypeById((int) entity.Property("Id").CurrentValue);
            Save();

            return new PASTypeConfirmation
            {
                Id = typeConfirmation.Id,
                Name = typeConfirmation.Name
            };
        }

        public PASTypeConfirmation UpdateType(PASType type)
        {
            var existing = GetTypeById(type.Id);

            try
            {
                existing.Id = type.Id;
                existing.Name = type.Name;

                this.dbContext.PASTypes.Update(existing);
                Save();

                var typeConfirmation = GetTypeById(type.Id);
                return new PASTypeConfirmation
                {
                    Id = type.Id,
                    Name = type.Name
                };
            } 
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteType(int id)
        {
            var type = this.dbContext.PASTypes.Find(id);
            this.dbContext.PASTypes.Remove(type);
            Save();
        }

        public void Save()
        {
            this.dbContext.SaveChanges();
        }
    }
}
