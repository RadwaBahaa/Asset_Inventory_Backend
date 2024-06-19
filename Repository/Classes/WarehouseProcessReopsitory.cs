using Context.Context;
using Models.Models;
using Repository.Interfaces;

namespace Repository.Classes
{
    public class WarehouseProcessReopsitory : GenericRepository<WarehouseProcess>, IWarehouseProcessReopsitory
    {
        protected AssetInventoryContext context;
        public WarehouseProcessReopsitory(AssetInventoryContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<IQueryable<WarehouseProcess>> ReadOneByID(int processID, int warehouseID)
        {
            var process = context.WarehouseProcesses
                .Where(p => p.ProcessID == processID && p.WarehouseID == warehouseID);
            return process;
        }
        public async Task<IQueryable<WarehouseProcess>> SearchByWarehouse(int warehouseID)
        {
            var processesList = context.WarehouseProcesses
                .Where(p => p.WarehouseID == warehouseID);
            return processesList;
        }
    }
}
