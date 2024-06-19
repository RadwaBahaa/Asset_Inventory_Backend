using Models.Models;

namespace Repository.Interfaces
{
    public interface IStoreProcessReopsitory : IGenericRepository<StoreProcess>
    {
        public Task<IQueryable<StoreProcess>> ReadOneByID(int processID, int storeID);
        public Task<IQueryable<StoreProcess>> SearchByStore(int storeID);
    }
}
