using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;

namespace Repository.Classes
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        protected AssetInventoryContext context;
        public CategoryRepository(AssetInventoryContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<Category> GetOneByID(int id)
        {
            var category = await context.Categories.FindAsync(id);
            return category;
        }
        public async Task<Category> GetOneByName(string name)
        {
            var category = await context.Categories.FirstOrDefaultAsync(a => a.CategoryName == name);
            return category;
        }
        public async Task<List<Category>> SearchByName(string name)
        {
            var categoryslist = await context.Categories
                .Where(a => a.CategoryName.ToLower().Contains(name.ToLower()))
                .ToListAsync();
            return categoryslist;
        }
    }
}
