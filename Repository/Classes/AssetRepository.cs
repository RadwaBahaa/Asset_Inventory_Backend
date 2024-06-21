using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;

namespace Repository.Classes
{
    public class AssetRepository : GenericRepository<Asset>, IAssetRepository
    {
        protected AssetInventoryContext context;
        public AssetRepository(AssetInventoryContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<Asset> ReadByID(int id)
        {
            var asset = await context.Assets
                .Include(a=>a.Category)
                .FirstOrDefaultAsync(a=>a.AssetID==id);
            return asset;
        }
        public async Task<Asset> ReadByName(string name)
        {
            var asset = await context.Assets
                .Include(a => a.Category)
                .FirstOrDefaultAsync(a => a.AssetName == name);
            return asset;
        }
        public async Task<List<Asset>> SearchByName(string name)
        {
            var assetsList = await context.Assets
                .Where(a => a.AssetName.ToLower().Contains(name.ToLower()))
                .ToListAsync();
            return assetsList;
        }
        public async Task<List<Asset>> SearchByCategory(Category category)
        {
            var assetsList = await context.Assets
                .Where(a => a.CategoryID == category.CategoryID)
                .ToListAsync();
            return assetsList;
        }
    }
}
