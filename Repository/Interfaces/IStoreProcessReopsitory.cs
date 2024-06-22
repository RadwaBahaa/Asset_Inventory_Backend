using Models.Models;

namespace Repository.Interfaces
{
    public interface IStoreProcessReopsitory : IGenericRepository<StoreProcess>
    {
        public Task<StoreProcess> ReadByID(int processID, int storeID);
        public Task<List<StoreProcess>> SearchByStore(int storeID);
    }
}
