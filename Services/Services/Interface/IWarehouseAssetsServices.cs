using DTOs.DTOs.Stores;
using DTOs.DTOs.Warehouses;

namespace Services.Services.Interface
{
    public interface IWarehouseAssetsServices
    {
        public Task<bool> CreateWarehouseAsset(AddOrUpdateWarehouseAssetsDTO addOrUpdateWarehouseAssetsDTO);
        public Task<List<ReadWarehouseAssetsDTO>> GetAllWarehouseAssets();
        public Task<ReadWarehouseAssetsDTO> GetOneBySerialNumber(string serialNumber);
        public Task<ReadWarehouseAssetsDTO> UpdateWarehouseAsset(AddOrUpdateWarehouseAssetsDTO addOrUpdateWarehouseAssetsDTO, int AssetID, int SerialNumber);
        public Task<bool> DeleteWarehouseAsset(int AssetID, int SerialNumber);

    }
}
