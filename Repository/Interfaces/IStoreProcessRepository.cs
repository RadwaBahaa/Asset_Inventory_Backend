using Models.Models;

namespace Repository.Interfaces
{
    public interface IStoreProcessRepository : IGenericRepository<StoreProcess>
    {
        public Task<StoreProcess> ReadByID(int processID, int storeID);
        public Task<List<StoreProcess>> ReadByStore(int storeID);
    }
}
