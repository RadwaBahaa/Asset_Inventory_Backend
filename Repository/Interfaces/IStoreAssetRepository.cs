using Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IStoreAssetRepository
    {
        Task<StoreAsset> GetOneByID(int assetID, int storeID);
        Task<StoreAsset> GetOneBySerialNumber(string serialNumber);
        Task<List<StoreAsset>> SearchByName(string assetName);
        Task<int> GetCount (int assetID, int storeID);
    }
}