using Models.Models;

namespace Repository.Interfaces
{
    public interface IDeliveryProcessSuWRepository : IGenericRepository<DeliveryProcessSuW>
    {
        public Task<IQueryable<DeliveryProcessSuW>> ReadOneByID(int id);
        public Task<IQueryable<DeliveryProcessSuW>> SearchBySupplier(int supplierID);
        public Task<IQueryable<DeliveryProcessSuW>> SearchByDate(DateTime date);
    }
}
