using DTOs.DTOs.Assets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;

namespace Presentation.Controllers
{
    [Route("api/assets")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        protected IAssetServices assetServices;
        public AssetsController(IAssetServices assetServices)
        {
            this.assetServices = assetServices;
        }

        // ___________________________ Create ___________________________
        [HttpPost("create")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create([FromBody] AddAssetDTO assetDTO)
        {
            if (assetDTO == null)
            {
                return BadRequest("Invalid input data. The request body must not be empty.");
            }
            try
            {
                var createAsset = await assetServices.Create(assetDTO);
                if (createAsset)
                {
                    return Ok("The asset was created successfully.");
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
        //[Authorize]
        //[Authorize(Roles = "Admin")]

        public async Task<IActionResult> ReadAll()
        {
            try
            {
                var assets = await assetServices.ReadAll();
                if (assets == null || !assets.Any())
                {
                    return NotFound("There are no assets.");
                }
                return Ok(assets);
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
        //[Authorize]
        public async Task<IActionResult> ReadByID([FromRoute] int id)
        {
            try
            {
                var asset = await assetServices.ReadByID(id);
                if (asset == null)
                {
                    return NotFound("There is no asset by this ID.");
                }
                return Ok(asset);
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
        //[Authorize]
        public async Task<IActionResult> Search([FromQuery] string? name, [FromQuery] string? categoryName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(categoryName))
                {
                    return BadRequest("Either 'name' or 'categoryName' must be provided.");
                }

                var assetsList = await assetServices.Search(name, categoryName);
                if (assetsList == null || !assetsList.Any())
                {
                    return NotFound("There are no assets.");
                }
                return Ok(assetsList);
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


        // ___________________________ Update ___________________________
        [HttpPut("update/{id}")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> Update([FromBody] UpdatAssetDTO assetDTO, [FromRoute] int id)
        {
            if (assetDTO == null)
            {
                return BadRequest("Invalid input data.");
            }
            try
            {
                var updateAsset = await assetServices.Update(assetDTO, id);
                return Ok(updateAsset);
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

        // ___________________________ Delete ___________________________
        [HttpDelete("delete/{id}")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var deleteAssets = await assetServices.Delete(id);
                return Ok($"The asset was deleted successfully.");
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
