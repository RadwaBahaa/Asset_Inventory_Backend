using Models.Models;

namespace Repository.Interfaces
{
    public interface IStoreRequestRepository :IGenericRepository<StoreRequest>
    {
        public Task<StoreRequest> GetByID(int id);
        public Task<List<StoreRequest>> SearchByStore(Store store);
        public Task<List<StoreRequest>> SearchByWarehouse(Warehouse warehouse);
        public Task<List<StoreRequest>> SearchByDate(DateTime date);
        public Task<List<StoreRequest>> SearchByStatus(string status);
    }
}
