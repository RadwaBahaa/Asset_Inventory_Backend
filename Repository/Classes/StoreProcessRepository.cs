using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;

namespace Repository.Classes
{
    public class StoreProcessRepository : GenericRepository<StoreProcess>, IStoreProcessRepository
    {
        protected AssetInventoryContext context;
        public StoreProcessRepository(AssetInventoryContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<StoreProcess> ReadByID(int processID, int storeID)
        {
            var process = await context.StoreProcesses
                .Include(p => p.AssetShipmentWSts)
                .FirstOrDefaultAsync(p => p.ProcessID == processID && p.StoreID == storeID);
            return process;
        }
        public async Task<List<StoreProcess>> SearchByStore(int storeID)
        {
            var processesList = await context.StoreProcesses
                .Include(p => p.AssetShipmentWSts)
                .Where(p => p.StoreID == storeID)
                .ToListAsync();
            return processesList;
        }
    }
}
