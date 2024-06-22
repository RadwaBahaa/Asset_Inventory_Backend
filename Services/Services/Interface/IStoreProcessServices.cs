using DTOs.DTOs.DeliveryProcesses;

namespace Services.Services.Interface
{
    public interface IStoreProcessServices
    {
        public Task<List<ReadStoreProcessDTO>> ReadAll();
        public Task<ReadStoreProcessDTO> ReadByID(int processID, int storeID);
        public Task<List<ReadStoreProcessDTO>> SearchByStore(int storeID);
        public Task<ReadStoreProcessDTO> Update(int processId, int storeID, UpdateStoreProcessDTO storeProcessDTO);
    }
}
