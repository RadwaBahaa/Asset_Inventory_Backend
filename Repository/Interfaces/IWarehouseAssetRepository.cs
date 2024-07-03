using Models.Models;

namespace Repository.Interfaces
{
    public interface IWarehouseAssetRepository : IGenericRepository<WarehouseAsset>
    {
        public Task<WarehouseAsset> ReadOne(int warehouseID, int? assetID, string? serialNumber);
        public Task<List<WarehouseAsset>> ReadByWarehouse(int warehouseID);
        public Task<List<WarehouseAsset>> Search(int warehouseID, string? name, string? serialNumber);

    }
}