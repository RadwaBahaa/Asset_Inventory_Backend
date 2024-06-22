using AutoMapper;
using DTOs.DTOs.Categories;
using Models.Models;
using Repository.Interfaces;
using Services.Services.Interface;


namespace Services.Services.Classes
{
    public class CategoryServices : ICategoryServices
    {
        protected ICategoryRepository categoryRepository;
        protected IMapper mapper;

        public CategoryServices(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        // __________________________ Create Category ___________________________
        public async Task<bool> Create(AddOrUpdateCategoryDTO categoryDTO)
        {
            if (categoryDTO == null || categoryDTO.CategoryName == null)
            {
                throw new AggregateException("There is no data in body");
            }
            else
            {
                var findcategory = await categoryRepository.ReadByName(categoryDTO.CategoryName);

                if (findcategory != null)
                {
                    throw new AggregateException("This Category already exists.");
                }
                else
                {
                    categoryDTO.CategoryName = char.ToUpper(categoryDTO.CategoryName[0]) + categoryDTO.CategoryName.Substring(1).ToLower();
                    var newCategory = mapper.Map<Category>(categoryDTO);
                    await categoryRepository.Create(newCategory);
                    return true;
                }
            }
        }

        // __________________________ Read Categories ___________________________
        public async Task<List<ReadCategoryDTO>> ReadAll()
        {
            var categories = await categoryRepository.Read();
            if (categories.Any())
            {
                return mapper.Map<List<ReadCategoryDTO>>(categories);
            }
            else
            {
                throw new AggregateException("There are no categories.");
            }
        }
        public async Task<ReadCategoryDTO> ReadByID(int ID)
        {
            var category = await categoryRepository.ReadByID(ID);
            if (category != null)
            {
                return mapper.Map<ReadCategoryDTO>(category);
            }
            else
            {
                throw new AggregateException("There is no category by this ID.");
            }
        }
        public async Task<ReadCategoryDTO> ReadByName(string name)
        {
            var category = await categoryRepository.ReadByName(name);
            if (category != null)
            {
                return mapper.Map<ReadCategoryDTO>(category);
            }
            else
            {
                throw new AggregateException("There is no category by this name.");
            }
        }

        // __________________________ Search categories by name ___________________________
        public async Task<List<ReadCategoryDTO>> SearchByName(string name)
        {
            var categoriesList = await categoryRepository.SearchByName(name);
            if (categoriesList.Any())
            {
                return mapper.Map<List<ReadCategoryDTO>>(categoriesList);
            }
            else
            {
                throw new AggregateException("There is no categories.");
            }
        }

        // __________________________ Update a Categories ___________________________
        public async Task<ReadCategoryDTO> Update(AddOrUpdateCategoryDTO categoryDTO, int ID)
        {
            var findcategory = await categoryRepository.ReadByID(ID);
            if (findcategory == null)
            {
                throw new AggregateException("There is no category by this ID.");
            }
            else
            {
                categoryDTO.CategoryName = char.ToUpper(categoryDTO.CategoryName[0]) + categoryDTO.CategoryName.Substring(1).ToLower();
                mapper.Map(categoryDTO, findcategory);
                await categoryRepository.Update();
                return mapper.Map<ReadCategoryDTO>(findcategory);
            }
        }

        // __________________________ Delete an asset ___________________________
        public async Task<bool> Delete(int ID)
        {
            var findcategory = await categoryRepository.ReadByID(ID);
            if (findcategory == null)
            {
                throw new AggregateException("There is no category by this ID.");
            }
            else
            {
                await categoryRepository.Delete(findcategory);
                return true;
            }
        }
    }
}


