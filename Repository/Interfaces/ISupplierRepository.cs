using Models.Models;

namespace Repository.Interfaces
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        public Task<Supplier> GetOneByID(int id);
        public Task<Supplier> GetOneByName(string name);
        public Task<List<Supplier>> SearchByName(string name);
        public Task<List<Supplier>> SearchByAddress(string address);
    }
}
