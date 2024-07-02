using DTOs.DTOs.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;

namespace Presentation.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryServices categoryServices;

        public CategoriesController(ICategoryServices categoryServices)
        {
            this.categoryServices = categoryServices;
        }


        // ___________________________ Create ___________________________
        [HttpPost("create")]
        [Authorize(Roles ="Admin")]
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
        [HttpGet("read")]
        [Authorize]
        public async Task<IActionResult> ReadAll()
        {
            try
            {
                var categories = await categoryServices.ReadAll();
                if (categories == null || !categories.Any())
                {
                    return NotFound("There are no categories.");
                }
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
        [HttpGet("read/{id}")]
        [Authorize]
        public async Task<IActionResult> ReadByID([FromRoute] int id)
        {
            try
            {
                var category = await categoryServices.ReadByID(id);
                if (category == null)
                {
                    return NotFound("There is no category by this id.");
                }
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
        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            try
            {
                var categoryList = await categoryServices.SearchByName(name);
                if (categoryList == null || !categoryList.Any())
                {
                    return NotFound("There are no categories.");
                }
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
        [HttpPut("update/{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Update([FromBody] AddOrUpdateCategoryDTO categoryDTO, [FromRoute] int id)
        {
            if (categoryDTO == null)
            {
                return BadRequest("Invalid input data.");
            }
            try
            {
                var updateCategory = await categoryServices.Update(categoryDTO, id);
                return Ok(updateCategory);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
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
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var deleteCategory = await categoryServices.Delete(id);
                return Ok($"The category was deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
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



