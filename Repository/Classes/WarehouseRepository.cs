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
                .Include(s => s.WarehouseAssets)
                .Include(s => s.WarehouseProcesses)
                .Include(s => s.WarehouseRequests)
                .Include(s=>s.DeliveryProcessWSt)
                .Include(s=>s.WarehouseProcesses)
                .FirstOrDefaultAsync(s => s.WarehouseID == id);
            return warehouse;
        }
        public async Task<Warehouse> ReadByName(string name)
        {
            var warehouse = await context.Warehouses
                .Include(s => s.WarehouseAssets)
                .Include(s => s.WarehouseProcesses)
                .Include(s => s.WarehouseRequests)
                .Include(s => s.DeliveryProcessWSt)
                .Include(s => s.WarehouseProcesses)
                .FirstOrDefaultAsync(a => a.WarehouseName == name);
            return warehouse;
        }
        public async Task<Warehouse> ReadByLocation(double? lon, double? lat)
        {
            var warehouse = await context.Warehouses
                .FirstOrDefaultAsync(a => a.Location.X == lon && a.Location.Y == lat);
            return warehouse;
        }
        public async Task<List<Warehouse>> SearchByName(string name)
        {
            var warehousesList = await context.Warehouses
                .Include(s => s.WarehouseAssets)
                .Include(s => s.WarehouseProcesses)
                .Include(s => s.WarehouseRequests)
                .Include(s => s.DeliveryProcessWSt)
                .Include(s => s.WarehouseProcesses)
                .Where(a => a.WarehouseName.ToLower().Contains(name.ToLower()))
                .ToListAsync();
            return warehousesList;
        }
        public async Task<List<Warehouse>> SearchByAddress(string address)
        {
            var warehousesList = await context.Warehouses
                .Include(s => s.WarehouseAssets)
                .Include(s => s.WarehouseProcesses)
                .Include(s => s.WarehouseRequests)
                .Include(s => s.DeliveryProcessWSt)
                .Include(s => s.WarehouseProcesses)
                .Where(a => a.Address.ToLower().Contains(address.ToLower()))
                .ToListAsync();
            return warehousesList;
        }
    }
}
