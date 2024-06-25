using DTOs.DTOs.Stores;

namespace Services.Services.Interface
{
    public interface IStoreAssetsServices
    {
        public Task<bool> Create(AddOrUpdateStoreAssetsDTO storeAssetsDTO);
        public Task<List<ReadStoreAssetsDTO>> ReadAll();
        public Task<List<ReadStoreAssetsDTO>> ReadBySerialNumber(string serialNumber);
        public Task<ReadStoreAssetsDTO> Update(AddOrUpdateStoreAssetsDTO addOrUpdateStoreAssetsDTO, int AssetID, int SerialNumber);
        public Task<bool> Delete(int AssetID, int SerialNumber);
    }
}
