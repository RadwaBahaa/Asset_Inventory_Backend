using Models.Models;

namespace Repository.Interfaces
{
    public interface IWarehouseRepository : IGenericRepository<Warehouse>
    {
        public Task<Warehouse> ReadByID(int id);
        public Task<Warehouse> ReadByName(string name);
        public Task<Warehouse> ReadByLocation(double? lon, double? lat);
        public Task<List<Warehouse>> SearchByName(string name);
        public Task<List<Warehouse>> SearchByAddress(string address);
    }
}
