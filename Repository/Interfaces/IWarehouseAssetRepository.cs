using Models.Models;

namespace Repository.Interfaces
{
    public interface IWarehouseAssetRepository
    {
        public Task<WarehouseAsset> ReadByID(int assetID, int warehouseID);
        public Task<WarehouseAsset> ReadBySerialNumber(string serialNumber);
        public Task<List<WarehouseAsset>> SearchByName(string assetName);
        public Task<int> GetCount(int assetID, int warehouseID);
    }
}
