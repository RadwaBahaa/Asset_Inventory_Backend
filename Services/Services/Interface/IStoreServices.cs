using DTOs.DTOs.Stores;
using Models.DTOs;

namespace Services.Services.Interface
{
    public interface IStoreServices
    {
        public Task<bool> CreateByData(AddStoreDTO storeDTO);
        public Task<bool> CreateByGeoJSON(AddStoreGeoJsonDTO storeDTO);
        public Task<List<ReadStoreDTO>> ReadAll();
        public Task<List<ReadStoreGeoJsonDTO>> ReadAllStoresAsGeoJson();
        public Task<ReadStoreDTO> ReadByID(int storeID);
        public Task<ReadStoreDTO> ReadByName(string name);
        public Task<ReadStoreGeoJsonDTO> ReadStoreAsGeoJson(int id);
        public Task<List<ReadStoreDTO>> Search(string name, string address);
        public Task<ReadStoreDTO> Update(UpdateStoreDTO storeDTO, int storeID);
        public Task<bool> Delete(int storeID);
    }
}
