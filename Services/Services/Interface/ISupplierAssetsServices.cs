using DTOs.DTOs.Suppliers;


namespace Services.Services.Interface
{
    public interface ISupplierAssetsServices
    {
        public Task<bool> CreateSupplierAsset(AddOrUpdateSupplierAssetsDTO addOrUpdateSupplierAssetsDTO);
        public Task<List<ReadSupplierAssetsDTO>> GetAllSupplierAssets();
        public Task<ReadSupplierAssetsDTO> GetOneBySerialNumber(string serialNumber);
        public Task<ReadSupplierAssetsDTO> UpdateSupplierAsset(AddOrUpdateSupplierAssetsDTO addOrUpdateSupplierAssetsDTO, int AssetID, int SerialNumber);
        public Task<bool> DeleteSupplierAsset(int AssetID, int SerialNumber);


    }
}
