using AutoMapper;
using DTOs.DTOs.DeliveryProcesses;
using Repository.Classes;

namespace Services.Services.Interface
{
    public interface IWarehouseProcessServices
    {
        public Task<List<ReadWarehouseProcessDTO>> ReadAllProcess();
        public Task<ReadWarehouseProcessDTO> ReadOneProcess(int processID, int warehouseID);
        public Task<List<ReadWarehouseProcessDTO>> SearchByWarehouse(int warehouseID);
        public Task<ReadWarehouseProcessDTO> UpdateProcess(int processId, int warehouseID, UpdateWarehouseProcessDTO warehouseProcessDTO);
    }
}
