using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;

namespace Repository.Classes
{
    public class StoreRepository : GenericRepository<Store>, IStoreRepository
    {
        protected AssetInventoryContext context;
        public StoreRepository(AssetInventoryContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<Store> ReadByID(int id)
        {
            var store = await context.Stores
                .Include(s => s.StoreAssets)
                .Include(s => s.StoreProcesses)
                .Include(s => s.StoreRequests)
                .FirstOrDefaultAsync(s => s.StoreID == id);
            return store;
        }
        public async Task<Store> ReadByName(string name)
        {
            var store = await context.Stores
                .Include(s => s.StoreAssets)
                .Include(s => s.StoreProcesses)
                .Include(s => s.StoreRequests)
                .FirstOrDefaultAsync(a => a.StoreName.ToLower() == name.ToLower());
            return store;
        }
        public async Task<Store> ReadByLocation(double? lon, double? lat)
        {
            var store = await context.Stores
                .FirstOrDefaultAsync(a => a.Location.X == lon && a.Location.Y == lat);
            return store;
        }
        public async Task<List<Store>> Search(string name, string address)
        {
            IQueryable<Store> stores = context.Stores
                .Include(s => s.StoreAssets)
                .Include(s => s.StoreProcesses)
                .Include(s => s.StoreRequests);

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(address))
            {
                stores = stores.Where(s => s.StoreName.ToLower().Contains(name.ToLower()) && s.Address.ToLower().Contains(address.ToLower()));
            }
            else if (!string.IsNullOrWhiteSpace(name))
            {
                stores = stores.Where(s => s.StoreName.ToLower().Contains(name.ToLower()));
            }
            else if (!string.IsNullOrWhiteSpace(address))
            {
                stores = stores.Where(s => s.Address.ToLower().Contains(address.ToLower()));
            }
            return await stores.ToListAsync();
        }
    }
}