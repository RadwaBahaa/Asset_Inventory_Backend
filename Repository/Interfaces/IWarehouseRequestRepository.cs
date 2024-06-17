using Models.Models;

namespace Repository.Interfaces
{
    public interface IWarehouseRequestRepository : IGenericRepository<WarehouseRequest>
    {
        public Task<WarehouseRequest> GetByID(int id);
        public Task<List<WarehouseRequest>> SearchByWarehouse(Warehouse warehouse);
        public Task<List<WarehouseRequest>> SearchBySupplier(Supplier supplier);
        public Task<List<WarehouseRequest>> SearchByDate(DateTime date);
        public Task<List<WarehouseRequest>> SearchByStatus(string status);
    }
}
