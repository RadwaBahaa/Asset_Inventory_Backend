using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;

namespace Repository.Classes
{
    public class StoreRequestRepository : GenericRepository<StoreRequest>, IStoreRequestRepository
    {
        protected AssetInventoryContext context;
        public StoreRequestRepository(AssetInventoryContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<StoreRequest> GetByID(int id)
        {
            var request = await context.StoreRequests.FindAsync(id);
            return request;
        }
        public async Task<List<StoreRequest>> SearchByStore(Store store)
        {
            var request = await context.StoreRequests
                .Where(sr=>sr.StoreID == store.StoreID)
                .ToListAsync();
            return request;
        }
        public async Task<List<StoreRequest>> SearchByWarehouse(Warehouse warehouse)
        {
            var request = await context.StoreRequests
                .Where(sr => sr.WarehouseID == warehouse.WarehouseID)
                .ToListAsync();
            return request;
        }
        public async Task<List<StoreRequest>> SearchByDate(DateTime date)
        {
            var request = await context.StoreRequests
                .Where(sr => sr.DateTime == date)
                .ToListAsync();
            return request;
        }
        public async Task<List<StoreRequest>> SearchByStatus(string status)
        {
            var request = await context.StoreRequests
                .Where(sr => sr.Status == status)
                .ToListAsync();
            return request;
        }
    }
}
