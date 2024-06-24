using Models.Models;

namespace Repository.Interfaces
{
    public interface IStoreRepository : IGenericRepository<Store>
    {
        public Task<Store> ReadByID(int id);
        public Task<Store> ReadByName(string name);
        public Task<Store> ReadByLocation(double? lon, double? lat);
        public Task<List<Store>> Search(string name, string address);
    }
}
