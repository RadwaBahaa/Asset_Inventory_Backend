using Models.Models;

namespace Repository.Interfaces
{
    public interface IStoreAssetRepository : IGenericRepository<StoreAsset>
    {
        public Task<StoreAsset> ReadByID(int assetID, int storeID);
        public Task<StoreAsset> ReadBySerialNumber(string serialNumber);
        public Task<List<StoreAsset>> SearchByName(string assetName);
        public Task<int> ReadCount(int assetID, int storeID);
    }
}