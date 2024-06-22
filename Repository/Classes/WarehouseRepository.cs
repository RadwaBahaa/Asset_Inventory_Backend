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
            var warehouse = await context.Warehouses.FindAsync(id);
            return warehouse;
        }
        public async Task<Warehouse> ReadByName(string name)
        {
            var warehouse = await context.Warehouses.FirstOrDefaultAsync(a => a.WarehouseName == name);
            return warehouse;
        }
        public async Task<List<Warehouse>> SearchByName(string name)
        {
            var warehouseList = await context.Warehouses
                .Where(a => a.WarehouseName.ToLower().Contains(name.ToLower()))
                .ToListAsync();
            return warehouseList;
        }
        public async Task<List<Warehouse>> SearchByAddress(string address)
        {
            var warehouseList = await context.Warehouses
                .Where(a => a.Address.ToLower().Contains(address.ToLower()))
                .ToListAsync();
            return warehouseList;
        }
    }
}
