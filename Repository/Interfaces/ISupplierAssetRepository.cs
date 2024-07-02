using Models.Models;

namespace Repository.Interfaces
{
    public interface ISupplierAssetRepository : IGenericRepository<SupplierAsset>
    {
        public Task<SupplierAsset> ReadOne(int supplierID, int assetID, string serialNumber);
        public Task<List<SupplierAsset>> ReadBySupplier(int supplierID);
        public Task<List<SupplierAsset>> Search(int supplierID, string? name, string? serialNumber);

    }
}