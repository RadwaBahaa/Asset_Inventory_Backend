using AutoMapper;
using DTOs.DTOs.Assets;
using DTOs.DTOs.Categories;
using Models.Models;
using Repository.Classes;
using Repository.Interfaces;
using Services.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<ReadCategoryDTO> Create(AddOrUpdateCategoryDTO categoryDTO)
        {
            var findcategory = await categoryRepository.GetOneByName(categoryDTO.CategoryName);

            if (findcategory != null)
            {
                return null;
            }
            else
            {
                categoryDTO.CategoryName = char.ToUpper(categoryDTO.CategoryName[0]) + categoryDTO.CategoryName.Substring(1).ToLower();
                var createCategory = mapper.Map<Category>(categoryDTO);
                await categoryRepository.Create(createCategory);
                var Category = await categoryRepository.GetOneByName(categoryDTO.CategoryName);

                var mappingcategory = mapper.Map<ReadCategoryDTO>(createCategory);
                return mappingcategory;
            }

        }
        // __________________________ GetAll categories ___________________________

        public async Task<List<ReadCategoryDTO>> GetAll()
        {
            var categories = await categoryRepository.GetAll();
            var mappedCategories = categories.Select(c => mapper.Map<ReadCategoryDTO>(c));
            var categoriesList = mappedCategories.ToList();
            return categoriesList;
        }





        // __________________________ GetOne By Name ___________________________

        public async Task<ReadCategoryDTO> GetOneByName(string name)
        {
            var category = await categoryRepository.GetOneByName(name);

            // Process the retrieved assets
            return mapper.Map<ReadCategoryDTO>(category);
        }



        // __________________________ Search categories by name ___________________________

        public async Task<List<ReadCategoryDTO>> SearchByName(string categoryName)
        {
            // Assuming categoryRepository has a method to search categories by name
            var categories = await categoryRepository.SearchByName(categoryName);

            // Mapping the found categories to ReadCategoryDTO
            var mappedCategories = categories.Select(c => mapper.Map<ReadCategoryDTO>(c)).ToList();

            return mappedCategories;
        }



        // __________________________ Update a Categories ___________________________
        public async Task<bool> Update(AddOrUpdateCategoryDTO categoryDTO, int ID)
        {
            var findcategory = await categoryRepository.GetOneByID(ID);
            if (findcategory == null)
            {
                return false;
            }
            else
            {
                // Make the initial letter of the asset name capitalized.
                categoryDTO.CategoryName = char.ToUpper(categoryDTO.CategoryName[0]) + categoryDTO.CategoryName.Substring(1).ToLower();
                // Update the asset
                mapper.Map(categoryDTO, findcategory);
                await categoryRepository.Update();
                return true;
            }
        }
        // __________________________ Delete an asset ___________________________
        public async Task<bool> Delete(int ID)
        {
            var findcategory = await categoryRepository.GetOneByID(ID);
            if (findcategory == null)
            {
                return false;
            }
            else
            {
                await categoryRepository.Delete(findcategory);
                return true;
            }
        }
    }
}

    
