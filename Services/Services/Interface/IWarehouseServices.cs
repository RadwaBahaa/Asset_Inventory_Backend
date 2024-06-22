using DTOs.DTOs.Warehouses;

namespace Services.Services.Interface
{
    public interface IWarehouseServices
    {
        public Task<bool> Create(AddOrUpdateWarehouseDTO warehouseDTO);
        public Task<List<ReadWarehouseDTO>> ReadAll();
        public Task<ReadWarehouseDTO> ReadByID(int warehouseID);
        public Task<List<ReadWarehouseDTO>> SearchByName(string warehouseName);
        public Task<List<ReadWarehouseDTO>> SearchByAddress(string Address);
        public Task<ReadWarehouseDTO> Update(AddOrUpdateWarehouseDTO updateWarehouseDTO, int warehouseID);
        public Task<bool> Delete(int warehouseID);
    }
}
}
