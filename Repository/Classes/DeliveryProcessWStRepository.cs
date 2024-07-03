using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;

namespace Repository.Classes
{
    public class DeliveryProcessWStRepository : GenericRepository<DeliveryProcessWSt>, IDeliveryProcessWStRepository
    {
        protected AssetInventoryContext context;
        public DeliveryProcessWStRepository(AssetInventoryContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<DeliveryProcessWSt> ReadByID(int ID)
        {
            var process = await context.DeliveryProcessWSt
                .Include(p => p.StoreProcesses)
                    .ThenInclude(sp => sp.AssetShipmentWSts)
                .FirstOrDefaultAsync(p => p.ProcessID == ID);
            return process;
        }
        public async Task<List<DeliveryProcessWSt>> SearchByWarehouse(int warehouseID)
        {
            var processesList = await context.DeliveryProcessWSt
                .Include(p => p.StoreProcesses)
                    .ThenInclude(sp => sp.AssetShipmentWSts)
                .Where(p => p.WarehouseID == warehouseID)
                .ToListAsync();
            return processesList;
        }
        public async Task<List<DeliveryProcessWSt>> SearchByDate(DateTime date)
        {
            var processesList = await context.DeliveryProcessWSt
                .Include(p => p.StoreProcesses)
                    .ThenInclude(sp => sp.AssetShipmentWSts)
                .Where(p => p.DateTime == date)
                .ToListAsync();
            return processesList;
        }
    }
}