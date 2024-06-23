using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;

namespace Repository.Classes
{
    public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
    {
        protected AssetInventoryContext context;
        public SupplierRepository(AssetInventoryContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<Supplier> ReadByID(int id)
        {
            var supplier = await context.Suppliers
                .Include(s => s.SupplierAssets)
                .Include(s => s.DeliveryProcessSuW)
                .Include(s => s.WarehouseRequests)
                .FirstOrDefaultAsync(s => s.SupplierID == id);
            return supplier;
        }
        public async Task<Supplier> ReadByName(string name)
        {
            var supplier = await context.Suppliers
                .Include(s => s.SupplierAssets)
                .Include(s => s.DeliveryProcessSuW)
                .Include(s => s.WarehouseRequests)
                .FirstOrDefaultAsync(a => a.SupplierName == name);
            return supplier;
        }
        public async Task<Supplier> ReadByLocation(double? lon, double? lat)
        {
            var supplier = await context.Suppliers
                .FirstOrDefaultAsync(a => a.Location.X == lon && a.Location.Y == lat);
            return supplier;
        }
        public async Task<List<Supplier>> SearchByName(string name)
        {
            var suppliersList = await context.Suppliers
                .Include(s => s.SupplierAssets)
                .Include(s => s.DeliveryProcessSuW)
                .Include(s => s.WarehouseRequests)
                .Where(a => a.SupplierName.ToLower().Contains(name.ToLower()))
                .ToListAsync();
            return suppliersList;
        }
        public async Task<List<Supplier>> SearchByAddress(string address)
        {
            var suppliersList = await context.Suppliers
                .Include(s => s.SupplierAssets)
                .Include(s => s.DeliveryProcessSuW)
                .Include(s => s.WarehouseRequests)
                .Where(a => a.Address.ToLower().Contains(address.ToLower()))
                .ToListAsync();
            return suppliersList;
        }
    }
}
