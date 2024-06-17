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
        public async Task<DeliveryProcessSuW> GetByID(int id)
        {
            var process = await context.DeliveryProcessSuW.FindAsync(id);
            return process;
        }
        public async Task<List<DeliveryProcessSuW>> SearchBySupplier(Supplier supplier)
        {
            var processesList = await context.DeliveryProcessSuW
                .Where(p => p.SupplierID == supplier.SupplierID)
                .ToListAsync();
            return processesList;
        }
        public async Task<List<DeliveryProcessSuW>> SearchByDate(DateTime date)
        {
            var processesList = await context.DeliveryProcessSuW
                .Where(p => p.DateTime == date)
                .ToListAsync();
            return processesList;
        }
    }
}
