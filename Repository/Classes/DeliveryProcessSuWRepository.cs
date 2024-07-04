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
                        .ThenInclude(ash => ash.SupplierAsset)
                            .ThenInclude(wa => wa.Asset)
                .FirstOrDefaultAsync(p => p.ProcessID == ID);
            return process;
        }
        public async Task<List<DeliveryProcessSuW>> Search(int? supplierID, DateTime? dateTime)
        {
            IQueryable<DeliveryProcessSuW> deliveryProcesses = context.DeliveryProcessSuW
                .Include(p => p.WarehouseProcesses)
                    .ThenInclude(sp => sp.AssetShipmentSuWs)
                        .ThenInclude(ash => ash.SupplierAsset)
                            .ThenInclude(wa => wa.Asset);

            if (supplierID != null && dateTime != null)
            {
                deliveryProcesses = deliveryProcesses.Where(dp => dp.SupplierID == supplierID && dp.DateTime.Date == dateTime.Value.Date);
            }
            else if (supplierID != null)
            {
                deliveryProcesses = deliveryProcesses.Where(dp => dp.SupplierID == supplierID);
            }
            else if (dateTime != null)
            {
                deliveryProcesses = deliveryProcesses.Where(dp => dp.DateTime.Date == dateTime.Value.Date);
            }

            return await deliveryProcesses.ToListAsync();
        }
    }
}
