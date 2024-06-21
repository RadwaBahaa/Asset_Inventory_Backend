using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Classes
{
    public class StoreAssetRepository : GenericRepository<StoreAsset>, IStoreAssetRepository
    {
        protected readonly AssetInventoryContext _context;

        public StoreAssetRepository(AssetInventoryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<StoreAsset> GetOneByID(int assetID, int storeID)
        {
            return await _context.StoreAssets.FindAsync(assetID, storeID);
        }

        public async Task<StoreAsset> GetOneBySerialNumber(string serialNumber)
        {
            return await _context.StoreAssets.FirstOrDefaultAsync(a => a.SerialNumber == serialNumber);
        }

        public async Task<List<StoreAsset>> SearchByName(string assetName)
        {
            return await _context.StoreAssets
                .Where(a => a.AssetName.ToLower().Contains(assetName.ToLower()))
                .ToListAsync();
        }

        public async Task<int> GetCount(int assetID, int storeID)
        {
            var storeAsset = await _context.StoreAssets
                .FirstOrDefaultAsync(a => a.AssetID == assetID && a.StoreID == storeID);

            if (storeAsset == null)
            {
                throw new KeyNotFoundException("StoreAsset not found");
            }

            return storeAsset.Count;
        }
    }
}
