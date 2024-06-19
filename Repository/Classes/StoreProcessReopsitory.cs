using Context.Context;
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
        public async Task<IQueryable<StoreProcess>> ReadOneByID(int processID, int storeID)
        {
            var process = context.StoreProcesses
                .Where(p => p.ProcessID == processID && p.StoreID == storeID);
            return process;
        }
        public async Task<IQueryable<StoreProcess>> SearchByStore(int storeID)
        {
            var processesList = context.StoreProcesses
                .Where(p => p.StoreID == storeID);
            return processesList;
        }
    }
}
