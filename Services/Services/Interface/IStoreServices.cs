using DTOs.DTOs.Stores;

namespace Services.Services.Interface
{
    public interface IStoreServices
    {
        public Task<bool> Create(AddOrUpdateStoreDTO storeDTO);
        public Task<List<ReadStoreDTO>> ReadAll();
        public Task<ReadStoreDTO> ReadByID(int storeID);
        public Task<List<ReadStoreDTO>> SearchByName(string storeName);
        public Task<List<ReadStoreDTO>> SearchByAddress(string Address);
        public Task<ReadStoreDTO> Update(AddOrUpdateStoreDTO updateStoreDTO, int storeID);
        public Task<bool> Delete(int storeID);
    }
}
