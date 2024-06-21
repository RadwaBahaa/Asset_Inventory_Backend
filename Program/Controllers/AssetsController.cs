using DTOs.DTOs.Assets;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Services.Services.Interface;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        protected IAssetServices assetServices;
        public AssetsController(IAssetServices assetServices)
        {
            this.assetServices = assetServices;
        }

        // ___________________________ Create ___________________________
        [HttpPost("/create")]
        public async Task<IActionResult> Create(AddOrUpdateAssetDTO assetDTO)
        {
            if (assetDTO == null)
            {
                return BadRequest("Invalid input data.");
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
        [HttpGet("/readOne/{id:int}")]
        public async Task<IActionResult> ReadOne(int id)
        {
            try
            {
                var asset = await assetServices.ReadByID(id);
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
        [HttpPut("/searchByName/{name:string}")]
        public async Task<IActionResult> SearchByName(string name)
        {
            try
            {
                var assetsList = await assetServices.SearchByName(name);
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
        [HttpPut("/searchByCategory/{category:string}")]
        public async Task<IActionResult> SearchByCategory(Category category)
        {
            try
            {
                var assetList = await assetServices.SearchByCategory(category);
                return Ok(assetList);
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
        [HttpPut("/update/{id:int}")]
        public async Task<IActionResult> Update(AddOrUpdateAssetDTO assetDTO, int id)
        {
            if (assetDTO == null)
            {
                return BadRequest("Invalid input data.");
            }
            try
            {
                var updateAsset = await assetServices.ReadByID(id);
                return Ok(updateAsset);
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
        [HttpDelete("/delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleteServices = await assetServices.Delete(id);
                return Ok($"The asset was deleted successfully.");
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
