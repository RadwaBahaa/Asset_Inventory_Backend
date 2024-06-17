using Models.Models;

namespace Repository.Interfaces
{
    public interface ISupplierRepository : IGenericRepository<Asset>
    {
        public Task<Asset> GetOneByID(int id);
        public Task<Asset> GetOneByName(string name);
        public Task<List<Asset>> Search(string name);
    }
}
