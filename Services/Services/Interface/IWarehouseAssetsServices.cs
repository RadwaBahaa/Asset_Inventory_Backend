using DTOs.DTOs.Warehouses;

namespace Services.Services.Interface
{
    public interface IWarehouseAssetsServices
    {
        public Task<bool> Create(AddOrUpdateWarehouseAssetsDTO warehouseAssetsDTO);
        public Task<List<ReadWarehouseAssetsDTO>> ReadAll();
        public Task<ReadWarehouseAssetsDTO> ReadBySerialNumber(string serialNumber);
        public Task<ReadWarehouseAssetsDTO> Update(AddOrUpdateWarehouseAssetsDTO addOrUpdateWarehouseAssetsDTO, int AssetID, int SerialNumber);
        public Task<bool> Delete(int AssetID, int SerialNumber);
    }
}
