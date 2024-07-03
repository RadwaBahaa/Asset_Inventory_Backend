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
        public async Task<WarehouseAsset> ReadOne(int warehouseID, int? assetID, string? serialNumber)
        {
            var warehouseAsset = await context.WarehouseAssets
                .Include(wa => wa.Asset)
                    .ThenInclude(a => a.Category)
                .FirstOrDefaultAsync(wa => wa.WarehouseID == warehouseID && wa.AssetID == assetID && wa.SerialNumber == serialNumber && wa.Count != 0);
            return warehouseAsset;
        }
        public async Task<List<WarehouseAsset>> ReadByWarehouse(int warehouseID)
        {
            var warehouseAsset = await context.WarehouseAssets
                .Include(wa => wa.Asset)
                    .ThenInclude(a => a.Category)
                .Where(wa => wa.WarehouseID == warehouseID && wa.Count != 0)
                .ToListAsync();
            return warehouseAsset;
        }
        public async Task<List<WarehouseAsset>> SearchByName(string assetName)
        {
            var warehouseAsset = await context.WarehouseAssets
                .Include(wa => wa.Asset)
                    .ThenInclude(a => a.Category)
                .Where(wa => wa.Asset.AssetName.ToLower().Contains(assetName.ToLower()) && wa.Count != 0)
                .ToListAsync();
            return warehouseAsset;
        }
        public async Task<List<WarehouseAsset>> Search(int warehouseID, string? name, string? serialNumber)
        {
            IQueryable<WarehouseAsset> warehouseAsset = context.WarehouseAssets
                .Include(wa => wa.Asset)
                    .ThenInclude(a => a.Category);
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(serialNumber))
            {
                warehouseAsset = warehouseAsset.Where(wa => wa.WarehouseID == warehouseID && wa.Asset.AssetName.ToLower().Contains(name.ToLower()) && wa.SerialNumber.ToLower().Contains(serialNumber.ToLower()) && wa.Count != 0);
            }
            else if (!string.IsNullOrWhiteSpace(name))
            {
                warehouseAsset = warehouseAsset.Where(wa => wa.WarehouseID == warehouseID && wa.Asset.AssetName.ToLower().Contains(name.ToLower()) && wa.Count != 0);
            }
            else
            {
                warehouseAsset = warehouseAsset.Where(wa => wa.WarehouseID == warehouseID && wa.SerialNumber.ToLower().Contains(serialNumber.ToLower()) && wa.Count != 0);
            }
            return await warehouseAsset.ToListAsync();
        }
    }
}
