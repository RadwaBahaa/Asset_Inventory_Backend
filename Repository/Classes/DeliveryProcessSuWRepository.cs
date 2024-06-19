using Context.Context;
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
        public async Task<IQueryable<DeliveryProcessSuW>> ReadOneByID(int id)
        {
            var process = context.DeliveryProcessSuW.Where(p => p.ProcessID == id);
            return process;
        }
        public async Task<IQueryable<DeliveryProcessSuW>> SearchBySupplier(int supplierID)
        {
            var processesList = context.DeliveryProcessSuW
                .Where(p => p.SupplierID == supplierID);
            return processesList;
        }
        public async Task<IQueryable<DeliveryProcessSuW>> SearchByDate(DateTime date)
        {
            var processesList = context.DeliveryProcessSuW
                .Where(p => p.DateTime == date);
            return processesList;
        }
    }
}
