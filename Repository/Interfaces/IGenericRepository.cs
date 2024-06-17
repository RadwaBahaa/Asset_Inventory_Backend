namespace Repository.Interfaces
{
    public interface IGenericRepository<Model>
    {
        public Task Create(Model model);
        public Task<IQueryable<Model>> Read();
        public Task Update();
        public Task Delete(Model model);
    }
}
