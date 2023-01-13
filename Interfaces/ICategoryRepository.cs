﻿using ExpenseTracker.Models;

namespace ExpenseTracker.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();

        Category GetCategory(int id);  
        Category GetCategory(string name);
        bool CategoryExists(int id);

        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        bool Save();
    }
}
