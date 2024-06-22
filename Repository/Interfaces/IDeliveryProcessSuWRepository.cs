using Models.Models;

namespace Repository.Interfaces
{
    public interface IDeliveryProcessSuWRepository : IGenericRepository<DeliveryProcessSuW>
    {
        public Task<DeliveryProcessSuW> ReadByID(int ID);
        public Task<List<DeliveryProcessSuW>> SearchBySupplier(int supplierID);
        public Task<List<DeliveryProcessSuW>> SearchByDate(DateTime date);
    }
}
