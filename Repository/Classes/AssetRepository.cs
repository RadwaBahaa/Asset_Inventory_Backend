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
        public async Task<Asset> GetOneByID(int id)
        {
            var asset = await context.Assets.FindAsync(id);
            return asset;
        }
        public async Task<Asset> GetOneByName(string name)
        {
            var asset = await context.Assets.FirstOrDefaultAsync(a => a.AssetName == name);
            return asset;
        }
        public async Task<List<Asset>> SearchByName(string name)
        {
            var assetslist = await context.Assets
                .Where(a => a.AssetName.ToLower().Contains(name.ToLower()))
                .ToListAsync();
            return assetslist;
        }
        public async Task<List<Asset>> SearchByCategory(Category category)
        {
            var assetslist = await context.Assets
                .Where(a => a.CategoryID == category.CategoryID)
                .ToListAsync();
            return assetslist;
        }
    }
}
