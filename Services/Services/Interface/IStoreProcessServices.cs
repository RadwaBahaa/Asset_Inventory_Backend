using DTOs.DTOs.DeliveryProcesses;

namespace Services.Services.Interface
{
    public interface IStoreProcessServices
    {
        public Task<List<ReadStoreProcessDTO>> ReadAllProcess();
        public Task<ReadStoreProcessDTO> ReadOneProcess(int processID, int storeID);
        public Task<List<ReadStoreProcessDTO>> SearchByStore(int storeID);
        public Task<ReadStoreProcessDTO> UpdateProcess(int processId, int storeID, UpdateStoreProcessDTO storeProcessDTO);
    }
}
