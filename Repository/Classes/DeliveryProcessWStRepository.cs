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
        public async Task<List<DeliveryProcessWSt>> Search(int? warehouseID, DateTime? dateTime)
        {
            IQueryable<DeliveryProcessWSt> deliveryProcesses = context.DeliveryProcessWSt
                .Include(p => p.StoreProcesses)
                    .ThenInclude(sp => sp.AssetShipmentWSts)
                        .ThenInclude(ash => ash.WarehouseAsset)
                            .ThenInclude(wa => wa.Asset);

            if (warehouseID != null && dateTime != null)
            {
                deliveryProcesses = deliveryProcesses.Where(dp => dp.WarehouseID == warehouseID && dp.DateTime.Date == dateTime.Value.Date);
            }
            else if (warehouseID != null)
            {
                deliveryProcesses = deliveryProcesses.Where(dp => dp.WarehouseID == warehouseID);
            }
            else if (dateTime != null)
            {
                deliveryProcesses = deliveryProcesses.Where(dp => dp.DateTime.Date == dateTime.Value.Date);
            }

            return await deliveryProcesses.ToListAsync();
        }
    }
}