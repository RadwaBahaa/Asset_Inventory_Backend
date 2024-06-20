using Context.Context;
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
        public async Task<IQueryable<DeliveryProcessWSt>> ReadOneByID(int id)
        {
            var process = context.DeliveryProcessWSt.Where(p => p.ProcessID == id);
            return process;
        }
        public async Task<IQueryable<DeliveryProcessWSt>> SearchByWarehouse(int warehouseID)
        {
            var processesList = context.DeliveryProcessWSt
                .Where(p => p.WarehouseID == warehouseID);
            return processesList;
        }
        public async Task<IQueryable<DeliveryProcessWSt>> SearchByDate(DateTime date)
        {
            var processesList = context.DeliveryProcessWSt
                .Where(p => p.DateTime == date);
            return processesList;
        }
    }
}
