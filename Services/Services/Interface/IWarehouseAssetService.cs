using DTOs.DTOs.Warehouses;

namespace Services.Services.Interface
{
    public interface IWarehouseAssetService
    {
        public Task<bool> CreateWarehouseAssets(AddOrUpdateWarehouseAssetsDTO addOrUpdateWarehouseAssetsDTO);
        public Task<ReadWarehouseAssetsDTO> GetAllWarehouseAssets();
        public Task<ReadWarehouseAssetsDTO> UpdateWarehouseAssets(AddOrUpdateWarehouseAssetsDTO addOrUpdateWarehouseAssetsDTO, int AssetID, int SerialNumber);
        public Task<ReadWarehouseAssetsDTO> DeleteWarehouseAssets(int AssetID, int SerialNumber);

    }
}
