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
        public async Task<Store> GetOneByID(int id)
        {
            var store = await context.Stores.FindAsync(id);
            return store;
        }
        public async Task<Store> GetOneByName(string name)
        {
            var store = await context.Stores.FirstOrDefaultAsync(a => a.StoreName == name);
            return store;
        }
        public async Task<List<Store>> SearchByName(string name)
        {
            var storesList = await context.Stores
                .Where(a => a.StoreName.ToLower().Contains(name.ToLower()))
                .ToListAsync();
            return storesList;
        }
        public async Task<List<Store>> SearchByAddress(string address)
        {
            var storesList = await context.Stores
                .Where(a => a.Address.ToLower().Contains(address.ToLower()))
                .ToListAsync();
            return storesList;
        }
    }
}
