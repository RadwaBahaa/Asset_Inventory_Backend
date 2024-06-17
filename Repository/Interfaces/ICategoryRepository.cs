using Models.Models;

namespace Repository.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        public Task<Category> GetOneByID(int id);
        public Task<Category> GetOneByName(string name);
        public Task<List<Category>> SearchByName(string name);
    }
}
