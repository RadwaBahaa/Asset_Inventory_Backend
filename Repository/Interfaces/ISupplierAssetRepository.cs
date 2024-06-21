using Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ISupplierAssetRepository
    {
        Task<SupplierAsset> GetOneByID(int assetID, int supplierID);
        Task<SupplierAsset> GetOneBySerialNumber(string serialNumber);
        Task<List<SupplierAsset>> SearchByName(string assetName);
        Task<int> GetCount(int assetID, int supplierID);
    }
}
