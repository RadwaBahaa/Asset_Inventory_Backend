using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;
using System.Diagnostics;

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
                .Include(p => p.Warehouse)
                .Include(p => p.StoreProcesses)
                    .ThenInclude(sp => sp.AssetShipment)
                        .ThenInclude(ash => ash.WarehouseAsset)
                            .ThenInclude(wa => wa.Asset)
                .Include(p => p.StoreProcesses)
                    .ThenInclude(sp => sp.Store)
                .FirstOrDefaultAsync(p => p.ProcessID == ID);
            return process;
        }
        public async Task<List<DeliveryProcessWSt>> ReadByWarehouse(int warehouseID)
        {
            var process = await context.DeliveryProcessWSt
                .Include(p => p.Warehouse)
                .Include(p => p.StoreProcesses)
                    .ThenInclude(sp => sp.Store)
                .Where(p => p.WarehouseID == warehouseID)
                .ToListAsync();
            return process;
        }
        public async Task<List<DeliveryProcessWSt>> ReadByStore(int storeID)
        {
            var processes = await context.DeliveryProcessWSt
                .Include(p => p.Warehouse)
                .Include(p => p.StoreProcesses)
                    .ThenInclude(sp => sp.AssetShipment)
                        .ThenInclude(ash => ash.WarehouseAsset)
                            .ThenInclude(wa => wa.Asset)
                .Include(p => p.StoreProcesses)
                    .ThenInclude(sp => sp.Store)
                //.FirstOrDefaultAsync(p => p.ProcessID == ID);
                .Where(p => p.StoreProcesses.Any(sp => sp.StoreID == storeID))
                .ToListAsync();

            foreach (var process in processes)
            {
                process.StoreProcesses = process.StoreProcesses
                    .Where(sp => sp.StoreID == storeID)
                    .ToList();
            }

            return processes;
        }
        public async Task<List<DeliveryProcessWSt>> Search(DateTime? dateTime)
        {
            var deliveryProcesses = await context.DeliveryProcessWSt
                .Include(p => p.Warehouse)
                .Include(p => p.StoreProcesses)
                    .ThenInclude(sp => sp.Store)
                .Where(dp => dp.DateTime.Date == dateTime.Value.Date)
                .ToListAsync();

            return deliveryProcesses;
        }
    }
}