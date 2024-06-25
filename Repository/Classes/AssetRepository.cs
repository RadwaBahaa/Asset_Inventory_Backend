using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;
using System.Net;

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
                .Include(a => a.Category)
                .FirstOrDefaultAsync(a => a.AssetID == id);
            return asset;
        }
        public async Task<Asset> ReadByName(string name)
        {
            var asset = await context.Assets
                .Include(a => a.Category)
                .FirstOrDefaultAsync(a => a.AssetName.ToLower() == name.ToLower());
            return asset;
        }
        public async Task<List<Asset>> Search(string? name, string? categoryName)
        {
            IQueryable<Asset> assetsList = context.Assets
                .Include(a => a.Category);
            if(!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(categoryName))
            {
                assetsList = assetsList.Where(a=>a.AssetName.ToLower().Contains(name.ToLower()) && a.Category.CategoryName.ToLower().Contains(categoryName.ToLower()));
            }else if (!string.IsNullOrWhiteSpace(name))
            {
                assetsList = assetsList.Where(a => a.AssetName.ToLower().Contains(name.ToLower()));
            }
            else
            {
                assetsList = assetsList.Where(a => a.Category.CategoryName.ToLower().Contains(categoryName.ToLower()));
            }
            return await assetsList.ToListAsync();
        }
    }
}
