using Models.Models;

namespace Repository.Interfaces
{
    public interface IDeliveryProcessWStRepository : IGenericRepository<DeliveryProcessWSt>
    {
        public Task<DeliveryProcessWSt> GetByID(int id);
        public Task<List<DeliveryProcessWSt>> SearchByWarehouse(Warehouse warehouse);
        public Task<List<DeliveryProcessWSt>> SearchByDate(DateTime date);
    }
}
