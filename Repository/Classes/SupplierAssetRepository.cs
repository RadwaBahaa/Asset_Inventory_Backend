﻿using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;

namespace Repository.Classes
{
    public class SupplierAssetRepository : GenericRepository<SupplierAsset>, ISupplierAssetRepository
    {
        protected AssetInventoryContext context;

        public SupplierAssetRepository(AssetInventoryContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<SupplierAsset> ReadOne(int supplierID, int assetID, string serialNumber)
        {
            var supplierAsset = await context.SupplierAssets
                .Include(sa => sa.Asset)
                    .ThenInclude(a => a.Category)
                .FirstOrDefaultAsync(sa => sa.SupplierID == supplierID && sa.AssetID == assetID && sa.SerialNumber == serialNumber);
            return supplierAsset;
        }
        public async Task<List<SupplierAsset>> ReadBySupplier(int supplierID)
        {
            var supplierAsset = await context.SupplierAssets
                .Include(sa => sa.Asset)
                    .ThenInclude(a => a.Category)
                .Where(sa => sa.SupplierID == supplierID)
                .ToListAsync();
            return supplierAsset;
        }
        public async Task<List<SupplierAsset>> SearchByName(string assetName)
        {
            var supplierAsset = await context.SupplierAssets
                .Include(sa => sa.Asset)
                    .ThenInclude(a => a.Category)
                .Where(a => a.Asset.AssetName.ToLower().Contains(assetName.ToLower()))
                .ToListAsync();
            return supplierAsset;
        }
        public async Task<List<SupplierAsset>> Search(int supplierID, string? name, string? serialNumber)
        {
            IQueryable<SupplierAsset> supplierAsset = context.SupplierAssets
                .Include(sa => sa.Asset)
                    .ThenInclude(a => a.Category);
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(serialNumber))
            {
                supplierAsset = supplierAsset.Where(sa => sa.SupplierID == supplierID && sa.Asset.AssetName.ToLower().Contains(name.ToLower()) && sa.SerialNumber.ToLower().Contains(serialNumber.ToLower()));
            }
            else if (!string.IsNullOrWhiteSpace(name))
            {
                supplierAsset = supplierAsset.Where(sa => sa.SupplierID == supplierID && sa.Asset.AssetName.ToLower().Contains(name.ToLower()));
            }
            else
            {
                supplierAsset = supplierAsset.Where(sa => sa.SupplierID == supplierID && sa.SerialNumber.ToLower().Contains(serialNumber.ToLower()));
            }
            return await supplierAsset.ToListAsync();
        }
    }
}
