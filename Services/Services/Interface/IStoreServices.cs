using DTOs.DTOs.Stores;
using Models.DTOs;

namespace Services.Services.Interface
{
    public interface IStoreServices
    {
        public Task<bool> Create(AddOrUpdateStoreDTO storeDTO);
        public Task<List<ReadStoreDTO>> ReadAll();
        public Task<List<StoreGeoJsonDTO>> ReadAllStoresAsGeoJson();
        public Task<ReadStoreDTO> ReadByID(int storeID);
        public Task<StoreGeoJsonDTO> ReadStoreAsGeoJson(int id);
        public Task<List<ReadStoreDTO>> SearchByName(string storeName);
        public Task<List<ReadStoreDTO>> SearchByAddress(string Address);
        public Task<ReadStoreDTO> Update(AddOrUpdateStoreDTO storeDTO, int storeID);
        public Task<bool> Delete(int storeID);
    }
}
