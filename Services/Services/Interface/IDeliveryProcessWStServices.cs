using DTOs.DTOs.DeliveryProcesses;

namespace Services.Services.Interface
{
    public interface IDeliveryProcessWStServices
    {
        public Task<bool> Create(AddDeliveryProcessWStDTO deliveryProcessWStDTO, int warehouseID);
        public Task<List<ReadDeliveryProcessWStDTO>> ReadAllProcess();
        public Task<ReadDeliveryProcessWStDTO> ReadOneByID(int id);
        public Task<List<ReadDeliveryProcessWStDTO>> SearchByWarehouse(int warehouseID);
        public Task<List<ReadDeliveryProcessWStDTO>> SearchByDate(DateTime date);
        public Task<bool> DeleteProcess(int processID);
    }
}

