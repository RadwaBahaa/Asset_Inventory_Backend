using Context.Context;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;

namespace Repository.Classes
{
    public class WarehouseRepository : GenericRepository<Warehouse>, IWarehouseRepository
    {
        protected AssetInventoryContext context;
        public WarehouseRepository(AssetInventoryContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<Warehouse> ReadByID(int id)
        {
            var warehouse = await context.Warehouses
                .FirstOrDefaultAsync(s => s.WarehouseID == id);
            return warehouse;
        }
        public async Task<Warehouse> ReadByName(string name)
        {
            var warehouse = await context.Warehouses
                .FirstOrDefaultAsync(a => a.WarehouseName.ToLower() == name.ToLower());
            return warehouse;
        }
        public async Task<Warehouse> ReadByLocation(double? lon, double? lat)
        {
            var warehouse = await context.Warehouses
                .FirstOrDefaultAsync(a => a.Location.X == lon && a.Location.Y == lat);
            return warehouse;
        }
        public async Task<List<Warehouse>> Search(string name, string address)
        {
            IQueryable<Warehouse> warehouses = context.Warehouses;

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(address))
            {
                warehouses = warehouses.Where(s => s.WarehouseName.ToLower().Contains(name.ToLower()) && s.Address.ToLower().Contains(address.ToLower()));
            }
            else if (!string.IsNullOrWhiteSpace(name))
            {
                warehouses = warehouses.Where(s => s.WarehouseName.ToLower().Contains(name.ToLower()));
            }
            else if (!string.IsNullOrWhiteSpace(address))
            {
                warehouses = warehouses.Where(s => s.Address.ToLower().Contains(address.ToLower()));
            }
            return await warehouses.ToListAsync();
        }
    }
}
