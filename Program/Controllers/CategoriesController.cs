using DTOs.DTOs.Assets;
using DTOs.DTOs.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Services.Services.Classes;
using Services.Services.Interface;


namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryServices categoryServices;

        public CategoriesController(ICategoryServices categoryServices)
        {
            this.categoryServices = categoryServices;
        }


        // ___________________________ 1- Create ___________________________
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(AddOrUpdateCategoryDTO categoryDTO)
        {
            var createcategory = await categoryServices.Create(categoryDTO);
            if (createcategory != null)
            {
                return Ok(createcategory);
            }
            else
            {
                return BadRequest("This category already exist !...");
            }
        }


        // ___________________________ 2- Get one Asset ___________________________
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetOne(string name)
        {
            var categories = await categoryServices.GetOneByName(name);
            if (categories == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(categories);
            }
        }


        // ___________________________ Get all categories ___________________________
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll()
        {
            var categories = await categoryServices.GetAll();
            if (categories == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(categories);
            }
        }





        // ___________________________ 3- to Search by Name And Category for Assets ___________________________

        public async Task<List<ReadCategoryDTO>> SearchByName(string categoryName)
        {
            var categoryList = await categoryServices.SearchByName(categoryName);
            return categoryList;
        }


        //edited ( cat controller, catservices and IcatServices)


        // ___________________________  Update ___________________________
        [HttpPut("{name:string}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(AddOrUpdateCategoryDTO categoryDTO, string name)
        {
            var UpdateCategory = await categoryServices.GetOneByName(name);
            if (UpdateCategory == null)
            {
                return BadRequest("There is no category with this name !....");
            }
            else
            {
                await categoryServices.Update(categoryDTO, name);
                return Ok($"The name of category {name} was updated from '{UpdateCategory.CategoryName}' to be '{categoryDTO.CategoryName}' ....");
            }
        }

        // ___________________________  Delete ___________________________

        [HttpDelete("{name:string}")]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string name)
        {
            var deleteServices = await categoryServices.Delete(name);
            if (deleteServices)
            {
                return Ok($"The category '{name}' was deleted successfully !....");
            }
            else
            {
                return BadRequest("There is no category with this name !....");
            }
        }

    }
}



