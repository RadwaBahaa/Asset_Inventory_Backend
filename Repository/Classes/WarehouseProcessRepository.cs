using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;

namespace Repository.Classes
{
    public class WarehouseProcessRepository : GenericRepository<WarehouseProcess>, IWarehouseProcessRepository
    {
        protected AssetInventoryContext context;
        public WarehouseProcessRepository(AssetInventoryContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<WarehouseProcess> ReadByID(int processID, int warehouseID)
        {
            var process = await context.WarehouseProcesses
                .Include(p => p.AssetShipmentSuWs)
                    .ThenInclude(ash=>ash.SupplierAsset)
                        .ThenInclude(sa=>sa.Asset)
                .FirstOrDefaultAsync(p => p.ProcessID == processID && p.WarehouseID == warehouseID);
            return process;
        }
        public async Task<List<WarehouseProcess>> SearchByWarehouse(int warehouseID)
        {
            var processesList = await context.WarehouseProcesses
                .Include(p => p.AssetShipmentSuWs)
                    .ThenInclude(ash => ash.SupplierAsset)
                        .ThenInclude(sa => sa.Asset)
                .Where(p => p.WarehouseID == warehouseID)
                .ToListAsync();
            return processesList;
        }
    }
}
