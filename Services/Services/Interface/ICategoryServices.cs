using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs.DTOs.Assets;
using DTOs.DTOs.Categories;

namespace Services.Services.Interface
{
    public interface ICategoryServices
    {
        public Task<ReadCategoryDTO> Create(AddOrUpdateCategoryDTO categoryDTO);//
        public Task<List<ReadCategoryDTO>> GetAll();//
        public Task<ReadCategoryDTO> GetOneByName(string name);//
        public Task<List<ReadCategoryDTO>> SearchByName(string categoryName);//
        public Task<bool> Update(AddOrUpdateCategoryDTO categoryDTO, int ID);//
        public Task<bool> Delete(int ID);//
        Task Update(AddOrUpdateCategoryDTO categoryDTO, string name);//
        Task<bool> Delete(string name);
    }
}
