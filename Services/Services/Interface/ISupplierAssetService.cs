using DTOs.DTOs.Suppliers;

namespace Services.Services.Interface
{
    public interface ISupplierAssetService
    {
        public Task<bool> CreateSupplierAssets(AddOrUpdateSupplierAssetsDTO addOrUpdateSupplierAssetsDTO);
        public Task<List<ReadSupplierAssetsDTO>> GetAllSupplierAssets();
        public Task<ReadSupplierAssetsDTO> UpdateSupplierAssets(AddOrUpdateSupplierAssetsDTO addOrUpdateSupplierAssetsDTO, int AssetID, int SerialNumber);
        public Task<bool> DeleteSupplierAssets(int AssetID, int SerialNumber);


    }
}
