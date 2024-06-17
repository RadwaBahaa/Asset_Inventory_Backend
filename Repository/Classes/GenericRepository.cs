using Context.Context;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Classes
{
    public class GenericRepository<Model> : IGenericRepository<Model> where Model : class
    {
        protected AssetInventoryContext context;
        protected DbSet<Model> models;
        public GenericRepository(AssetInventoryContext context)
        {
            this.context = context;
            models = context.Set<Model>();
        }
        public async Task Create(Model model)
        {
            await models.AddAsync(model);
            await context.SaveChangesAsync();
        }
        public async Task<IQueryable<Model>> Read()
        {
            return await Task.FromResult(models.Select(m => m));
        }
        public async Task Update()
        {
            await context.SaveChangesAsync();
        }
        public async Task Delete(Model model)
        {
            models.Remove(model);
            await context.SaveChangesAsync();
        }
    }
}
