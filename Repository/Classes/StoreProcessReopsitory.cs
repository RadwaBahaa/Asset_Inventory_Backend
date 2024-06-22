using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;

namespace Repository.Classes
{
    public class StoreProcessReopsitory : GenericRepository<StoreProcess>, IStoreProcessReopsitory
    {
        protected AssetInventoryContext context;
        public StoreProcessReopsitory(AssetInventoryContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<StoreProcess> ReadByID(int processID, int storeID)
        {
            var process = await context.StoreProcesses
                .Include(p => p.AssetShipmentWSt)
                .FirstOrDefaultAsync(p => p.ProcessID == processID && p.StoreID == storeID);
            return process;
        }
        public async Task<List<StoreProcess>> SearchByStore(int storeID)
        {
            var processesList = await context.StoreProcesses
                .Include(p => p.AssetShipmentWSt)
                .Where(p => p.StoreID == storeID)
                .ToListAsync();
            return processesList;
        }
    }
}
