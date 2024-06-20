using DTOs.DTOs.Warehouses;

namespace Services.Services.Interface
{
    public interface IWarehouseAssetService
    {
        public Task<bool> CreateWarehouseAssets(AddOrUpdateWarehouseAssetsDTO addOrUpdateWarehouseAssetsDTO);
        public Task<List<ReadWarehouseAssetsDTO>> GetAllWarehouseAssets();
        public Task<ReadWarehouseAssetsDTO> UpdateWarehouseAssets(AddOrUpdateWarehouseAssetsDTO addOrUpdateWarehouseAssetsDTO, int AssetID, int SerialNumber);
        public Task<bool> DeleteWarehouseAssets(int AssetID, int SerialNumber);

    }
}
