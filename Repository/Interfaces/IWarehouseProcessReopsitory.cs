using Models.Models;

namespace Repository.Interfaces
{
    public interface IWarehouseProcessReopsitory : IGenericRepository<WarehouseProcess>
    {
        public Task<IQueryable<WarehouseProcess>> ReadOneByID(int processID, int warehouseID);
        public Task<IQueryable<WarehouseProcess>> SearchByWarehouse(int warehouseID);
    }
}
