using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;

namespace Repository.Classes
{
    public class StoreAssetRepository : GenericRepository<StoreAsset>, IStoreAssetRepository
    {
        protected AssetInventoryContext context;

        public StoreAssetRepository(AssetInventoryContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<StoreAsset> ReadByID(int assetID, int storeID)
        {
            var storeAsset = await context.StoreAssets
                .FirstOrDefaultAsync(sa => sa.StoreID == storeID && sa.AssetID == assetID);
            return storeAsset;
        }
        public async Task<StoreAsset> ReadBySerialNumber(string serialNumber)
        {
            var storeAsset = await context.StoreAssets.FirstOrDefaultAsync(sa => sa.SerialNumber == serialNumber);
            return storeAsset;
        }
        public async Task<List<StoreAsset>> SearchByName(string assetName)
        {
            var storeAsset = await context.StoreAssets
                .Where(a => a.AssetName.ToLower().Contains(assetName.ToLower()))
                .ToListAsync();
            return storeAsset;
        }
        public async Task<int> ReadCount(int assetID, int storeID)
        {
            var storeAsset = await context.StoreAssets
                .FirstOrDefaultAsync(a => a.AssetID == assetID && a.StoreID == storeID);
            return storeAsset.Count;
        }
    }
}
