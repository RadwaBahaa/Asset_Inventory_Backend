using DTOs.DTOs.Suppliers;

namespace Services.Services.Interface
{
    public interface ISupplierAssetsServices
    {
        public Task<bool> Create(int supplierID, AddSupplierAssetsDTO supplierAssetsDTO);
        public Task<List<ReadSupplierAssetsDTO>> ReadAll();
        public Task<List<ReadSupplierAssetsDTO>> ReadBySupplier(int supplierID);
        public Task<List<ReadSupplierAssetsDTO>> Search(int supplierID, string? name, string? serialNumber);
        public Task<ReadSupplierAssetsDTO> Update(UpdateSupplierAssetsDTO supplierAssetsDTO, int supplierID, int assetID, string serialNumber);
        public Task<bool> Delete(int supplierID, int assetID, string serialNumber);
    }
}