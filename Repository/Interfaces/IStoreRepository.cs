using Models.Models;

namespace Repository.Interfaces
{
    public interface IStoreRepository : IGenericRepository<Store>
    {
        public Task<Store> GetOneByID(int id);
        public Task<Store> GetOneByName(string name);
        public Task<List<Store>> SearchByName(string name);
        public Task<List<Store>> SearchByAddress(string address);
    }
}
