using DTOs.DTOs.Stores;

namespace Services.Services.Interface
{
    public interface IStoreAssetsServices
    {
        public Task<bool> Create(int storeID, AddStoreAssetsDTO storeAssetsDTO);
        public Task<List<ReadStoreAssetsDTO>> ReadAll();
        public Task<List<ReadStoreAssetsDTO>> ReadByStore(int storeID);
        public Task<List<ReadStoreAssetsDTO>> Search(int storeID, string? name, string? serialNumber);
        public Task<ReadStoreAssetsDTO> Update(UpdateStoreAssetsDTO storeAssetsDTO, int storeID, int assetID, string serialNumber);
        public Task<bool> Delete(int storeID, int assetID, string serialNumber);
    }
}
