using Models.Models;

namespace Repository.Interfaces
{
    public interface IDeliveryProcessWStRepository : IGenericRepository<DeliveryProcessWSt>
    {
        public Task<DeliveryProcessWSt> ReadByID(int ID);
        public Task<List<DeliveryProcessWSt>> Search(int? warehouseID, DateTime? dateTime);
    }
}
