using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Classes
{
    public class SupplierAssetRepository : GenericRepository<SupplierAsset>, ISupplierAssetRepository
    {
        private readonly AssetInventoryContext _context;

        public SupplierAssetRepository(AssetInventoryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SupplierAsset> GetOneByID(int assetID, int supplierID)
        {
            return await _context.SupplierAssets.FindAsync(assetID, supplierID);
        }

        public async Task<SupplierAsset> GetOneBySerialNumber(string serialNumber)
        {
            return await _context.SupplierAssets.FirstOrDefaultAsync(a => a.SerialNumber == serialNumber);
        }

        public async Task<List<SupplierAsset>> SearchByName(string assetName)
        {
            return await _context.SupplierAssets
                .Where(a => a.AssetName.ToLower().Contains(assetName.ToLower()))
                .ToListAsync();
        }

        public async Task<int> GetCount(int assetID, int supplierID)
        {
            var supplierAsset = await _context.SupplierAssets
                .FirstOrDefaultAsync(a => a.AssetID == assetID && a.SupplierID == supplierID);

            if (supplierAsset == null)
            {
                throw new KeyNotFoundException("SupplierAsset not found");
            }

            return supplierAsset.Count;
        }
    }
}
