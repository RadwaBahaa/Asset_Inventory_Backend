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
                .Include(p => p.WarehouseProcesses)
                    .ThenInclude(wp => wp.AssetShipmentSuWs)
                .FirstOrDefaultAsync(p => p.ProcessID == ID);
            return process;
        }
        public async Task<List<DeliveryProcessSuW>> SearchBySupplier(int supplierID)
        {
            var processesList = await context.DeliveryProcessSuW
                .Include(p => p.WarehouseProcesses)
                    .ThenInclude(wp => wp.AssetShipmentSuWs)
                .Where(p => p.SupplierID == supplierID)
                .ToListAsync();
            return processesList;
        }
        public async Task<List<DeliveryProcessSuW>> SearchByDate(DateTime date)
        {
            var processesList = await context.DeliveryProcessSuW
                .Include(p => p.WarehouseProcesses)
                    .ThenInclude(wp => wp.AssetShipmentSuWs)
                .Where(p => p.DateTime == date)
                .ToListAsync();
            return processesList;
        }
    }
}
