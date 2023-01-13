using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;

namespace ExpenseTracker.Repository
{
    public class CategoryRepository : ICategoryRepository
        { 
            private readonly ApplicationDbContext _context;   
            public CategoryRepository(ApplicationDbContext context)
            {
                _context = context; 
            }

        public ICollection<Category> GetCategories()
            {
                return _context.Categories.OrderBy(c => c.CategoryId).ToList();
            }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(p => p.CategoryId == id).FirstOrDefault();
        }

        public Category GetCategory(string name)
        {
            return _context.Categories.Where(p => p.CategoryName == name).FirstOrDefault(); 
        }

        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.CategoryId == id);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ?  true : false;
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
            
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }
        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
        }
    }  
}
