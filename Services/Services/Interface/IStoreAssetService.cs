using DTOs.DTOs.Stores;

namespace Services.Services.Interface
{
    public interface IStoreAssetService
    {
        public Task<bool> CreateStoreAssets(AddOrUpdateStoreAssetsDTO addOrUpdateStoreAssetsDTO);
        public Task<ReadStoreAssetsDTO> ReadStoreAssets();
        public Task<ReadStoreAssetsDTO> UpdateStoreAssets(AddOrUpdateStoreAssetsDTO addOrUpdateStoreAssetsDTO, int AssetID,int SerialNumber);
        public Task<ReadStoreAssetsDTO> DeleteStoreAssets(int AssetID, int SerialNumber);

    }
}
