using Models.Models;

namespace Repository.Interfaces
{
    public interface IStoreAssetRepository : IGenericRepository<StoreAsset>
    {
        public Task<StoreAsset> ReadOne(int storeID, int assetID, string serialNumber);
        public Task<List<StoreAsset>> ReadByStore(int storeID);
        public Task<List<StoreAsset>> Search(int storeID, string? name, string? serialNumber);

    }
}