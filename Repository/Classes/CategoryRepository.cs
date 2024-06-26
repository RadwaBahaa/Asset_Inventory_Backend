﻿using Context.Context;
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

        public async Task<Category> ReadByID(int id)
        {
            var category = await context.Categories
                .FindAsync(id);
            return category;
        }
        public async Task<Category> ReadByName(string name)
        {
            var category = await context.Categories
                .FirstOrDefaultAsync(a => a.CategoryName.ToLower() == name.ToLower());
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
