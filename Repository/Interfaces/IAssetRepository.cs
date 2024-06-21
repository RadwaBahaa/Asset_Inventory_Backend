using Models.Models;

namespace Repository.Interfaces
{
    public interface IAssetRepository : IGenericRepository<Asset>
    {
        public Task<Asset> ReadByID(int id);
        public Task<Asset> ReadByName(string name);
        public Task<List<Asset>> SearchByName(string name);
        public Task<List<Asset>> SearchByCategory(int categoryID);
    }
}
