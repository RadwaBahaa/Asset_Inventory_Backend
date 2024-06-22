using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;

namespace Repository.Classes
{
    public class SupplierAssetRepository : GenericRepository<SupplierAsset>, ISupplierAssetRepository
    {
        private readonly AssetInventoryContext context;

        public SupplierAssetRepository(AssetInventoryContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<SupplierAsset> ReadByID(int assetID, int supplierID)
        {
            var supplierAsset = await context.SupplierAssets
                .FirstOrDefaultAsync(sa => sa.SupplierID == supplierID && sa.AssetID == assetID);
            return supplierAsset;
        }
        public async Task<SupplierAsset> ReadBySerialNumber(string serialNumber)
        {
            var supplierAsset = await context.SupplierAssets.FirstOrDefaultAsync(sa => sa.SerialNumber == serialNumber);
            return supplierAsset;
        }
        public async Task<List<SupplierAsset>> SearchByName(string assetName)
        {
            var supplierAsset = await context.SupplierAssets
                .Where(a => a.AssetName.ToLower().Contains(assetName.ToLower()))
                .ToListAsync();
            return supplierAsset;
        }
        public async Task<int> ReadCount(int assetID, int supplierID)
        {
            var supplierAsset = await context.SupplierAssets
                .FirstOrDefaultAsync(a => a.AssetID == assetID && a.SupplierID == supplierID);
            return supplierAsset.Count;
        }
    }
}
