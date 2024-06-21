using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Classes
{
    public class WarehouseAssetRepository : GenericRepository<WarehouseAsset>, IWarehouseAssetRepository
    {
        protected readonly AssetInventoryContext _context;

        public WarehouseAssetRepository(AssetInventoryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<WarehouseAsset> GetOneByID(int assetID, int warehouseID)
        {
            return await _context.WarehouseAssets.FindAsync(assetID, warehouseID);
        }

        public async Task<WarehouseAsset> GetOneBySerialNumber(string serialNumber)
        {
            return await _context.WarehouseAssets.FirstOrDefaultAsync(a => a.SerialNumber == serialNumber);
        }

        public async Task<List<WarehouseAsset>> SearchByName(string assetName)
        {
            return await _context.WarehouseAssets
                .Where(a => a.AssetName.ToLower().Contains(assetName.ToLower()))
                .ToListAsync();
        }

        public async Task<int> GetCount(int assetID, int warehouseID)
        {
            var warehouseAsset = await _context.WarehouseAssets
                .FirstOrDefaultAsync(a => a.AssetID == assetID && a.WarehouseID == warehouseID);

            if (warehouseAsset == null)
            {
                throw new KeyNotFoundException("WarehouseAsset not found");
            }

            return warehouseAsset.Count;
        }
    }
}
