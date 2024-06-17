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
        public async Task<DeliveryProcessWSt> GetByID(int id)
        {
            var process = await context.DeliveryProcessWSt.FindAsync(id);
            return process;
        }
        public async Task<List<DeliveryProcessWSt>> SearchByWarehouse(Warehouse warehouse)
        {
            var processesList = await context.DeliveryProcessWSt
                .Where(p => p.WarehouseID == warehouse.WarehouseID)
                .ToListAsync();
            return processesList;
        }
        public async Task<List<DeliveryProcessWSt>> SearchByDate(DateTime date)
        {
            var processesList = await context.DeliveryProcessWSt
                .Where(p => p.DateTime == date)
                .ToListAsync();
            return processesList;
        }
    }
}
