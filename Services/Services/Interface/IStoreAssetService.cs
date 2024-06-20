using DTOs.DTOs.Stores;

namespace Services.Services.Interface
{
    public interface IStoreAssetService
    {
        public Task<bool> CreateStoreAssets(AddOrUpdateStoreAssetsDTO addOrUpdateStoreAssetsDTO);
        public Task<List<ReadStoreAssetsDTO>> GetAllStoreAssets();
        public Task<ReadStoreAssetsDTO> UpdateStoreAssets(AddOrUpdateStoreAssetsDTO addOrUpdateStoreAssetsDTO, int AssetID,int SerialNumber);
        public Task<bool> DeleteStoreAssets(int AssetID, int SerialNumber);

    }
}
