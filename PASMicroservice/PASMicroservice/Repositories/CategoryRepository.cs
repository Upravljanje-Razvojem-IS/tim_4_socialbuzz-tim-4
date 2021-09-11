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
            var categoryConfirmation = GetCategoryById(category.CategoryId);
            Save();

            return new CategoryConfirmation
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                ParentCategoryId = category.ParentCategoryId
            };
        }

        public CategoryConfirmation UpdateCategory(Category category)
        {
            var existing = GetCategoryById(category.CategoryId);

            try
            {
                existing.CategoryId = category.CategoryId;
                existing.Name = category.Name;
                existing.ParentCategoryId = category.ParentCategoryId;

                this.dbContext.Categories.Update(existing);
                Save();

                var categoryConfirmation = GetCategoryById(category.CategoryId);

                return new CategoryConfirmation
                {
                    CategoryId = categoryConfirmation.CategoryId,
                    Name = categoryConfirmation.Name,
                    ParentCategoryId = categoryConfirmation.ParentCategoryId
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
