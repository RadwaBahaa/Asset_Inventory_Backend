using Models.Models;

namespace Repository.Interfaces
{
    public interface IWarehouseRepository : IGenericRepository<Warehouse>
    {
        public Task<Warehouse> GetOneByID(int id);
        public Task<Warehouse> GetOneByName(string name);
        public Task<List<Warehouse>> SearchByName(string name);
        public Task<List<Warehouse>> SearchByAddress(string address);
    }
}
