using Models.Models;

namespace Repository.Interfaces
{
    public interface IDeliveryProcessWStRepository : IGenericRepository<DeliveryProcessWSt>
    {
        public Task<IQueryable<DeliveryProcessWSt>> ReadOneByID(int id);
        public Task<IQueryable<DeliveryProcessWSt>> SearchByWarehouse(int warehouseID);
        public Task<IQueryable<DeliveryProcessWSt>> SearchByDate(DateTime date);
    }
}
