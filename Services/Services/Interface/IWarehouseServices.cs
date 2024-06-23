using DTOs.DTOs.Warehouses;
using Models.DTOs;

namespace Services.Services.Interface
{
    public interface IWarehouseServices
    {
        public Task<bool> Create(AddOrUpdateWarehouseDTO warehouseDTO);
        public Task<List<ReadWarehouseDTO>> ReadAll();
        public Task<List<WarehouseGeoJsonDTO>> ReadAllWarehousesAsGeoJson();
        public Task<ReadWarehouseDTO> ReadByID(int warehouseID);
        public Task<WarehouseGeoJsonDTO> ReadWarehouseAsGeoJson(int id);
        public Task<List<ReadWarehouseDTO>> SearchByName(string warehouseName);
        public Task<List<ReadWarehouseDTO>> SearchByAddress(string Address);
        public Task<ReadWarehouseDTO> Update(AddOrUpdateWarehouseDTO warehouseDTO, int warehouseID);
        public Task<bool> Delete(int warehouseID);
    }
}