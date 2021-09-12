using System;
using System.Collections.Generic;
using PASMicroservice.Entities;

namespace PASMicroservice.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
        Category GetCategoryById(Guid id);
        CategoryConfirmation CreateCategory(Category category);
        CategoryConfirmation UpdateCategory(Category category);
        void DeleteCategory(Guid id);
        void Save();
    }
}
