using DTOs.DTOs.DeliveryProcesses;

namespace Services.Services.Interface
{
    public interface IWarehouseProcessServices
    {
        public Task<List<ReadWarehouseProcessDTO>> ReadAll();
        public Task<ReadWarehouseProcessDTO> ReadByID(int processID, int warehouseID);
        public Task<List<ReadWarehouseProcessDTO>> ReadByWarehouse(int warehouseID);
        public Task<bool> Update(int processId, int warehouseID, string userRole);
    }
}
