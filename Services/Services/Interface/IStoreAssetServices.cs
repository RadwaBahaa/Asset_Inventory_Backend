using DTOs.DTOs.Stores;

namespace Services.Services.Interface
{
    public interface IStoreAssetServices
    {
        public Task<bool> CreateStoreAsset(AddOrUpdateStoreAssetsDTO addOrUpdateStoreAssetsDTO);
        public Task<List<ReadStoreAssetsDTO>> GetAllStoreAssets();
        public Task<ReadStoreAssetsDTO> GetOneBySerialNumber(string serialNumber);
        public Task<ReadStoreAssetsDTO> UpdateStoreAsset(AddOrUpdateStoreAssetsDTO addOrUpdateStoreAssetsDTO, int AssetID,int SerialNumber);
        public Task<bool> DeleteStoreAsset(int AssetID, int SerialNumber);

    }
}
