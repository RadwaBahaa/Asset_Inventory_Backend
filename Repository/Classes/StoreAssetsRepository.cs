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
        public async Task<StoreAsset> ReadOne(int storeID, int assetID, string serialNumber)
        {
            var storeAsset = await context.StoreAssets
                .Include(sa => sa.Asset)
                    .ThenInclude(a => a.Category)
                .FirstOrDefaultAsync(sa => sa.StoreID == storeID && sa.AssetID == assetID && sa.SerialNumber == serialNumber);
            return storeAsset;
        }
        public async Task<List<StoreAsset>> ReadByStore(int storeID)
        {
            var storeAsset = await context.StoreAssets
                .Include(sa => sa.Asset)
                .ThenInclude(a => a.Category)
                .Where(sa => sa.StoreID == storeID)
                .ToListAsync();
            return storeAsset;
        }
        public async Task<List<StoreAsset>> SearchByName(string assetName)
        {
            var storeAsset = await context.StoreAssets
                .Include(sa => sa.Asset)
                    .ThenInclude(a => a.Category)
                .Where(a => a.Asset.AssetName.ToLower().Contains(assetName.ToLower()))
                .ToListAsync();
            return storeAsset;
        }
        public async Task<List<StoreAsset>> Search(int storeID, string? name, string? serialNumber)
        {
            IQueryable<StoreAsset> storeAsset = context.StoreAssets
                .Include(sa => sa.Asset)
                    .ThenInclude(a => a.Category);
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(serialNumber))
            {
                storeAsset = storeAsset.Where(sa => sa.StoreID == storeID && sa.Asset.AssetName.ToLower().Contains(name.ToLower()) && sa.SerialNumber.ToLower().Contains(serialNumber.ToLower()));
            }
            else if (!string.IsNullOrWhiteSpace(name))
            {
                storeAsset = storeAsset.Where(sa => sa.StoreID == storeID && sa.Asset.AssetName.ToLower().Contains(name.ToLower()));
            }
            else
            {
                storeAsset = storeAsset.Where(sa => sa.StoreID == storeID && sa.SerialNumber.ToLower().Contains(serialNumber.ToLower()));
            }
            return await storeAsset.ToListAsync();
        }
    }
}
