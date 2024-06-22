using DTOs.DTOs.Categories;

namespace Services.Services.Interface
{
    public interface ICategoryServices
    {
        public Task<bool> Create(AddOrUpdateCategoryDTO categoryDTO);
        public Task<List<ReadCategoryDTO>> ReadAll();
        public Task<ReadCategoryDTO> ReadByID(int ID);
        public Task<ReadCategoryDTO> ReadByName(string name);
        public Task<List<ReadCategoryDTO>> SearchByName(string name);
        public Task<ReadCategoryDTO> Update(AddOrUpdateCategoryDTO categoryDTO, int ID);
        public Task<bool> Delete(int ID);
    }
}
