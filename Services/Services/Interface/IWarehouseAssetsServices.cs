using DTOs.DTOs.Warehouses;

namespace Services.Services.Interface
{
    public interface IWarehouseAssetsServices
    {
        public Task<bool> Create(int warehouseID, AddWarehouseAssetsDTO warehouseAssetsDTO);
        public Task<List<ReadWarehouseAssetsDTO>> ReadAll();
        public Task<List<ReadWarehouseAssetsDTO>> ReadByWarehouse(int warehouseID);
        public Task<List<ReadWarehouseAssetsDTO>> Search(int warehouseID, string? name, string? serialNumber);
        public Task<ReadWarehouseAssetsDTO> Update(UpdateWarehouseAssetsDTO warehouseAssetsDTO, int warehouseID, int assetID, string serialNumber);
        public Task<bool> Delete(int warehouseID, int assetID, string serialNumber);
    }
}