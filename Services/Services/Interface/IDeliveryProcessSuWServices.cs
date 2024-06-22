using DTOs.DTOs.DeliveryProcesses;

namespace Services.Services.Interface
{
    public interface IDeliveryProcessSuWServices
    {
        public Task<bool> Create(AddDeliveryProcessSuWDTO addDeliveryProcessSuWDTO, int supplierID);
        public Task<List<ReadDeliveryProcessSuWDTO>> ReadAll();
        public Task<ReadDeliveryProcessSuWDTO> ReadByID(int ID);
        public Task<List<ReadDeliveryProcessSuWDTO>> SearchBySupplier(int supplierID);
        public Task<List<ReadDeliveryProcessSuWDTO>> SearchByDate(DateTime date);
        public Task<bool> DeleteProcess(int processID);
    }
}

