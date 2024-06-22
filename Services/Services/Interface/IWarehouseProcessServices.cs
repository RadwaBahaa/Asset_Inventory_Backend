using DTOs.DTOs.DeliveryProcesses;

namespace Services.Services.Interface
{
    public interface IWarehouseProcessServices
    {
        public Task<List<ReadWarehouseProcessDTO>> ReadAll();
        public Task<ReadWarehouseProcessDTO> ReadByID(int processID, int warehouseID);
        public Task<List<ReadWarehouseProcessDTO>> SearchByWarehouse(int warehouseID);
        public Task<ReadWarehouseProcessDTO> Update(int processId, int warehouseID, UpdateWarehouseProcessDTO warehouseProcessDTO);
    }
}
