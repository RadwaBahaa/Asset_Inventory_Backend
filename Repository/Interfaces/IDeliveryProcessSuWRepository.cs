using Models.Models;

namespace Repository.Interfaces
{
    public interface IDeliveryProcessSuWRepository : IGenericRepository<DeliveryProcessSuW>
    {
        public Task<DeliveryProcessSuW> GetByID(int id);
        public Task<List<DeliveryProcessSuW>> SearchBySupplier(Supplier supplier);
        public Task<List<DeliveryProcessSuW>> SearchByDate(DateTime date);
    }
}
