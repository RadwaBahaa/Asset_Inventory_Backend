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
                .FirstOrDefaultAsync(a => a.StoreName == name);
            return store;
        }
        public async Task<Store> ReadByLocation(double? lon, double? lat)
        {
            var store = await context.Stores
                .FirstOrDefaultAsync(a => a.Location.X == lon && a.Location.Y == lat);
            return store;
        }
        public async Task<List<Store>> SearchByName(string name)
        {
            var storesList = await context.Stores
                .Include(s => s.StoreAssets)
                .Include(s => s.StoreProcesses)
                .Include(s => s.StoreRequests)
                .Where(a => a.StoreName.ToLower().Contains(name.ToLower()))
                .ToListAsync();
            return storesList;
        }
        public async Task<List<Store>> SearchByAddress(string address)
        {
            var storesList = await context.Stores
                .Include(s => s.StoreAssets)
                .Include(s => s.StoreProcesses)
                .Include(s => s.StoreRequests)
                .Where(a => a.Address.ToLower().Contains(address.ToLower()))
                .ToListAsync();
            return storesList;
        }
    }
}