using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PASMicroservice.DBContexts;
using PASMicroservice.Entities;

namespace PASMicroservice.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PASContext dbContext;

        public CategoryRepository(PASContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Category> GetCategories()
        {
            return this.dbContext.Categories.ToList();
        }

        public Category GetCategoryById(Guid id)
        {
            return this.dbContext.Categories.Find(id);
        }

        public CategoryConfirmation CreateCategory(Category category)
        {
            this.dbContext.Categories.Add(category);
            var categoryConfirmation = GetCategoryById(category.Id);
            Save();

            return new CategoryConfirmation
            {
                Id = category.Id,
                Name = category.Name,
                ParentId = category.ParentId,
                TypeId = category.TypeId
            };
        }

        public CategoryConfirmation UpdateCategory(Category category)
        {
            var existing = GetCategoryById(category.Id);

            try
            {
                existing.Id = category.Id;
                existing.Name = category.Name;
                existing.ParentId = category.ParentId;
                existing.TypeId = category.TypeId;

                this.dbContext.Categories.Update(existing);
                Save();

                var categoryConfirmation = GetCategoryById(category.Id);

                return new CategoryConfirmation
                {
                    Id = categoryConfirmation.Id,
                    Name = categoryConfirmation.Name,
                    ParentId = categoryConfirmation.ParentId,
                    TypeId = categoryConfirmation.TypeId
            };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteCategory(Guid id)
        {
            var category = this.dbContext.Categories.Find(id);
            this.dbContext.Categories.Remove(category);
            Save();
        }

        public void Save()
        {
            this.dbContext.SaveChanges();
        }
    }
}
