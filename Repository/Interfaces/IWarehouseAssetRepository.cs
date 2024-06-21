using Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IWarehouseAssetRepository
    {
        Task<WarehouseAsset> GetOneByID(int assetID, int warehouseID);
        Task<WarehouseAsset> GetOneBySerialNumber(string serialNumber);
        Task<List<WarehouseAsset>> SearchByName(string assetName);
        Task<int> GetCount(int assetID, int warehouseID);
    }
}
