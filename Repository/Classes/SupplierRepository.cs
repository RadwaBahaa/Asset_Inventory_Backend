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
                .FirstOrDefaultAsync(a => a.SupplierName.ToLower() == name.ToLower());
            return supplier;
        }
        public async Task<Supplier> ReadByLocation(double? lon, double? lat)
        {
            var supplier = await context.Suppliers
                .FirstOrDefaultAsync(a => a.Location.X == lon && a.Location.Y == lat);
            return supplier;
        }
        public async Task<List<Supplier>> Search(string name, string address)
        {
            IQueryable<Supplier> suppliers = context.Suppliers
                .Include(s => s.SupplierAssets)
                .Include(s => s.DeliveryProcessSuW)
                .Include(s => s.WarehouseRequests);

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(address))
            {
                suppliers = suppliers.Where(s => s.SupplierName.ToLower().Contains(name.ToLower()) && s.Address.ToLower().Contains(address.ToLower()));
            }
            else if (!string.IsNullOrWhiteSpace(name))
            {
                suppliers = suppliers.Where(s => s.SupplierName.ToLower().Contains(name.ToLower()));
            }
            else if (!string.IsNullOrWhiteSpace(address))
            {
                suppliers = suppliers.Where(s => s.Address.ToLower().Contains(address.ToLower()));
            }
            return await suppliers.ToListAsync();
        }
    }
}
