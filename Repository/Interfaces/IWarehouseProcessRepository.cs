using Models.Models;

namespace Repository.Interfaces
{
    public interface IWarehouseProcessRepository : IGenericRepository<WarehouseProcess>
    {
        public Task<WarehouseProcess> ReadByID(int processID, int warehouseID);
        public Task<List<WarehouseProcess>> ReadByWarehouse(int warehouseID);
    }
}
