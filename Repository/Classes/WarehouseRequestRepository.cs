using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;

namespace Repository.Classes
{
    public class WarehouseRequestRepository : GenericRepository<WarehouseRequest>, IWarehouseRequestRepository
    {
        protected AssetInventoryContext context;
        public WarehouseRequestRepository(AssetInventoryContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<WarehouseRequest> GetByID(int id)
        {
            var request = await context.WarehouseRequests.FindAsync(id);
            return request;
        }
        public async Task<List<WarehouseRequest>> SearchByWarehouse(Warehouse warehouse)
        {
            var request = await context.WarehouseRequests
                .Where(wr=>wr.WarehouseID == warehouse.WarehouseID)
                .ToListAsync();
            return request;
        }
        public async Task<List<WarehouseRequest>> SearchBySupplier(Supplier supplier)
        {
            var request = await context.WarehouseRequests
                .Where(wr => wr.SupplierID == supplier.SupplierID)
                .ToListAsync();
            return request;
        }
        public async Task<List<WarehouseRequest>> SearchByDate(DateTime date)
        {
            var request = await context.WarehouseRequests
                .Where(wr => wr.DateTime == date)
                .ToListAsync();
            return request;
        }
        public async Task<List<WarehouseRequest>> SearchByStatus(string status)
        {
            var request = await context.WarehouseRequests
                .Where(wr => wr.Status == status)
                .ToListAsync();
            return request;
        }
    }
}
