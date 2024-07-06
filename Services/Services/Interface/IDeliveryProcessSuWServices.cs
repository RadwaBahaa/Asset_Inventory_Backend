using DTOs.DTOs.DeliveryProcesses;

namespace Services.Services.Interface
{
    public interface IDeliveryProcessSuWServices
    {
        public Task<bool> Create(AddDeliveryProcessSuWDTO deliveryProcessSuWDTO, int supplierID);
        public Task<List<ReadDeliveryProcessSuWDTO>> ReadAll();
        public Task<ReadDeliveryProcessSuWDTO> ReadByID(int ID);
        public Task<List<ReadDeliveryProcessSuWDTO>> ReadBySupplier(int supplierID);
        public Task<List<ReadDeliveryProcessSuWDTO>> Search(DateTime? date);
        public Task<bool> DeleteProcess(int processID);
    }
}