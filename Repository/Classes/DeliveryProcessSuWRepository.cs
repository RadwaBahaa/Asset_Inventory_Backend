using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;

namespace Repository.Classes
{
    public class DeliveryProcessSuWRepository : GenericRepository<DeliveryProcessSuW>, IDeliveryProcessSuWRepository
    {
        protected AssetInventoryContext context;
        public DeliveryProcessSuWRepository(AssetInventoryContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<DeliveryProcessSuW> ReadByID(int ID)
        {
            var process = await context.DeliveryProcessSuW
                .Include(p => p.Supplier)
                .Include(p => p.WarehouseProcesses)
                    .ThenInclude(wp => wp.Warehouse)
                .FirstOrDefaultAsync(p => p.ProcessID == ID);
            return process;
        }
        public async Task<List<DeliveryProcessSuW>> ReadBySupplier(int supplierID)
        {
            var process = await context.DeliveryProcessSuW
                .Include(p => p.Supplier)
                .Include(p => p.WarehouseProcesses)
                    .ThenInclude(wp => wp.Warehouse)
                .Where(p => p.SupplierID == supplierID)
                .ToListAsync();
            return process;
        }
        public async Task<List<DeliveryProcessSuW>> ReadByWarehouse(int warehouseID)
        {
            var processes = await context.DeliveryProcessSuW
                .Include(p => p.Supplier)
                .Include(p => p.WarehouseProcesses)
                    .ThenInclude(sp => sp.Warehouse)
                .Where(p => p.WarehouseProcesses.Any(sp => sp.WarehouseID == warehouseID))
                .ToListAsync();

            foreach (var process in processes)
            {
                process.WarehouseProcesses = process.WarehouseProcesses
                    .Where(sp => sp.WarehouseID == warehouseID)
                    .ToList();
            }

            return processes;
        }
        public async Task<List<DeliveryProcessSuW>> Search(DateTime? dateTime)
        {
            var deliveryProcesses = await context.DeliveryProcessSuW
                .Include(p => p.Supplier)
                .Include(p => p.WarehouseProcesses)
                    .ThenInclude(wp => wp.Warehouse)
                .Where(dp => dp.DateTime.Date == dateTime.Value.Date)
                .ToListAsync();

            return deliveryProcesses;
        }
    }
}