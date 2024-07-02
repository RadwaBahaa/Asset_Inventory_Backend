using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;

namespace Repository.Classes
{
    public class WarehouseAssetRepository : GenericRepository<WarehouseAsset>, IWarehouseAssetRepository
    {
        protected AssetInventoryContext context;

        public WarehouseAssetRepository(AssetInventoryContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<WarehouseAsset> ReadOne(int warehouseID, int assetID, string serialNumber)
        {
            var warehouseAsset = await context.WarehouseAssets
                .Include(sa => sa.Asset)
                    .ThenInclude(a => a.Category)
                .FirstOrDefaultAsync(sa => sa.WarehouseID == warehouseID && sa.AssetID == assetID && sa.SerialNumber == serialNumber);
            return warehouseAsset;
        }
        public async Task<List<WarehouseAsset>> ReadByWarehouse(int warehouseID)
        {
            var warehouseAsset = await context.WarehouseAssets
                .Include(sa => sa.Asset)
                .ThenInclude(a => a.Category)
                .Where(sa => sa.WarehouseID == warehouseID)
                .ToListAsync();
            return warehouseAsset;
        }
        public async Task<List<WarehouseAsset>> SearchByName(string assetName)
        {
            var warehouseAsset = await context.WarehouseAssets
                .Include(sa => sa.Asset)
                    .ThenInclude(a => a.Category)
                .Where(a => a.Asset.AssetName.ToLower().Contains(assetName.ToLower()))
                .ToListAsync();
            return warehouseAsset;
        }
        public async Task<List<WarehouseAsset>> Search(int warehouseID, string? name, string? serialNumber)
        {
            IQueryable<WarehouseAsset> warehouseAsset = context.WarehouseAssets
                .Include(sa => sa.Asset)
                    .ThenInclude(a => a.Category);
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(serialNumber))
            {
                warehouseAsset = warehouseAsset.Where(sa => sa.WarehouseID == warehouseID && sa.Asset.AssetName.ToLower().Contains(name.ToLower()) && sa.SerialNumber.ToLower().Contains(serialNumber.ToLower()));
            }
            else if (!string.IsNullOrWhiteSpace(name))
            {
                warehouseAsset = warehouseAsset.Where(sa => sa.WarehouseID == warehouseID && sa.Asset.AssetName.ToLower().Contains(name.ToLower()));
            }
            else
            {
                warehouseAsset = warehouseAsset.Where(sa => sa.WarehouseID == warehouseID && sa.SerialNumber.ToLower().Contains(serialNumber.ToLower()));
            }
            return await warehouseAsset.ToListAsync();
        }
    }
}
