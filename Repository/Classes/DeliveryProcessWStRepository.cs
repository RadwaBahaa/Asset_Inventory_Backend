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
                        .ThenInclude(ash=>ash.WarehouseAsset)
                            .ThenInclude(wa=>wa.Asset)
                .FirstOrDefaultAsync(p => p.ProcessID == ID);
            return process;
        }
        public async Task<List<DeliveryProcessWSt>> ReadByWarehouse(int warehouseID)
        {
            var process = await context.DeliveryProcessWSt
                .Include(p => p.StoreProcesses)
                    .ThenInclude(sp => sp.AssetShipmentWSts)
                        .ThenInclude(ash => ash.WarehouseAsset)
                            .ThenInclude(wa => wa.Asset)
                .Where(p => p.WarehouseID == warehouseID)
                .ToListAsync();
            return process;
        }
        public async Task<List<DeliveryProcessWSt>> Search(DateTime? dateTime)
        {
            var deliveryProcesses = await context.DeliveryProcessWSt
                .Include(p => p.StoreProcesses)
                    .ThenInclude(sp => sp.AssetShipmentWSts)
                        .ThenInclude(ash => ash.WarehouseAsset)
                            .ThenInclude(wa => wa.Asset)
                .Where(dp=> dp.DateTime.Date == dateTime.Value.Date)
                .ToListAsync();

            return deliveryProcesses;
        }
    }
}