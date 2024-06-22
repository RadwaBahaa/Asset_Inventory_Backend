using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;

namespace Repository.Classes
{
    public class WarehouseAssetRepository : GenericRepository<WarehouseAsset>, IWarehouseAssetRepository
    {
        protected readonly AssetInventoryContext context;

        public WarehouseAssetRepository(AssetInventoryContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<WarehouseAsset> ReadByID(int assetID, int warehouseID)
        {
            var warehouseAsset = await context.WarehouseAssets
                .FirstOrDefaultAsync(wa => wa.AssetID == assetID && wa.WarehouseID == warehouseID);
            return warehouseAsset;
        }
        public async Task<WarehouseAsset> ReadBySerialNumber(string serialNumber)
        {
            var warehouseAsset = await context.WarehouseAssets
               .FirstOrDefaultAsync(a => a.SerialNumber == serialNumber);
            return warehouseAsset;
        }
        public async Task<List<WarehouseAsset>> SearchByName(string assetName)
        {
            var warehouseAsset = await context.WarehouseAssets
                .Where(a => a.AssetName.ToLower().Contains(assetName.ToLower()))
                .ToListAsync();
            return warehouseAsset;
        }
        public async Task<int> GetCount(int assetID, int warehouseID)
        {
            var warehouseAsset = await context.WarehouseAssets
                .FirstOrDefaultAsync(a => a.AssetID == assetID && a.WarehouseID == warehouseID);
            return warehouseAsset.Count;
        }
    }
}
