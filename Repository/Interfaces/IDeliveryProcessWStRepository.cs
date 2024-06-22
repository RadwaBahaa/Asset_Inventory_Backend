using Models.Models;

namespace Repository.Interfaces
{
    public interface IDeliveryProcessWStRepository : IGenericRepository<DeliveryProcessWSt>
    {
        public Task<DeliveryProcessWSt> ReadByID(int ID);
        public Task<List<DeliveryProcessWSt>> SearchByWarehouse(int warehouseID);
        public Task<List<DeliveryProcessWSt>> SearchByDate(DateTime date);
    }
}
