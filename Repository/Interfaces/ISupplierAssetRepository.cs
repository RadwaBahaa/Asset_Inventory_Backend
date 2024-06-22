using Models.Models;

namespace Repository.Interfaces
{
    public interface ISupplierAssetRepository
    {
        public Task<SupplierAsset> ReadByID(int assetID, int supplierID);
        public Task<SupplierAsset> ReadBySerialNumber(string serialNumber);
        public Task<List<SupplierAsset>> SearchByName(string assetName);
        public Task<int> ReadCount(int assetID, int supplierID);
    }
}
