using Models.Models;

namespace Repository.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        public Task<Category> ReadByID(int id);
        public Task<Category> ReadByName(string name);
        public Task<List<Category>> SearchByName(string name);
    }
}
