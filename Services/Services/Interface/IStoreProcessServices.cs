using DTOs.DTOs.DeliveryProcesses;

namespace Services.Services.Interface
{
    public interface IStoreProcessServices
    {
        public Task<List<ReadStoreProcessDTO>> ReadAll();
        public Task<ReadStoreProcessDTO> ReadByID(int processID, int storeID);
        public Task<List<ReadStoreProcessDTO>> ReadByStore(int storeID);
        public Task<bool> Update(int processId, int storeID, string userRole);
    }
}
