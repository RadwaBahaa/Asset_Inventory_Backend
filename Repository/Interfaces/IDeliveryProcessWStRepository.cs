using Models.Models;

namespace Repository.Interfaces
{
    public interface IDeliveryProcessWStRepository : IGenericRepository<DeliveryProcessWSt>
    {
        public Task<DeliveryProcessWSt> ReadByID(int ID);
        public  Task<List<DeliveryProcessWSt>> ReadByWarehouse(int warehouseID);
        public Task<List<DeliveryProcessWSt>> ReadByStore(int storeID);
        public Task<List<DeliveryProcessWSt>> Search(DateTime? dateTime);
    }
}
