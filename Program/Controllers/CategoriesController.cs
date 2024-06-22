using DTOs.DTOs.Categories;
using Microsoft.AspNetCore.Mvc;
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


        // ___________________________ Create ___________________________
        [HttpPost("/create")]
        public async Task<IActionResult> Create([FromBody] AddOrUpdateCategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                return BadRequest("Invalid input data.");
            }
            try
            {
                var createcategory = await categoryServices.Create(categoryDTO);
                if (createcategory)
                {
                    return Ok("The category was created successfully.");
                }
                else
                {
                    return NotFound("Process not found.");
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // ___________________________ Read ___________________________
        [HttpGet("/readAll")]
        public async Task<IActionResult> ReadAll()
        {
            try
            {
                var categories = await categoryServices.ReadAll();
                return Ok(categories);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("/readByID/{ID:int}")]
        public async Task<IActionResult> ReadByID([FromRoute] int ID)
        {
            try
            {
                var category = await categoryServices.ReadByID(ID);
                return Ok(category);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // __________________________ Search __________________________
        [HttpPut("/searchByName/{name:string}")]
        public async Task<IActionResult> SearchByName([FromRoute] string name)
        {
            try
            {
                var categoryList = await categoryServices.SearchByName(name);
                return Ok(categoryList);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // ___________________________  Update ___________________________
        [HttpPut("/update/{ID:int}")]
        public async Task<IActionResult> Update([FromBody] AddOrUpdateCategoryDTO categoryDTO, [FromRoute] int ID)
        {
            if (categoryDTO == null)
            {
                return BadRequest("Invalid input data.");
            }
            try
            {
                var updateCategory = await categoryServices.Update(categoryDTO, ID);
                return Ok(updateCategory);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // ___________________________  Delete ___________________________
        [HttpDelete("/delete/{ID:int}")]
        public async Task<IActionResult> Delete([FromRoute] int ID)
        {
            try
            {
                var deleteCategory = await categoryServices.Delete(ID);
                return Ok($"The category was deleted successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}



