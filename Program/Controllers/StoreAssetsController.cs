using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;
using DTOs.DTOs.Stores;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StoreAssetsController : ControllerBase
    {
        protected IStoreAssetsServices storeAssetServices;
        public StoreAssetsController(IStoreAssetsServices storeAssetServices)
        {
            this.storeAssetServices = storeAssetServices;
        }

        // __________________________ Create __________________________
        [HttpPost("create")]
        public async Task<IActionResult> CreateStoreAsset([FromBody] AddOrUpdateStoreAssetsDTO storeAssetsDTO)
        {
            if (storeAssetsDTO == null)
            {
                return BadRequest("Invalid input data.");
            }
            try
            {
                var storeAsset = await storeAssetServices.Create(storeAssetsDTO);
                if (storeAsset)
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

        // __________________________ Read __________________________
        [HttpGet("read")]
        public async Task<IActionResult> ReadAll()
        {
            try
            {
                var storeAssets = await storeAssetServices.ReadAll();
                if (storeAssets == null || storeAssets.Any())
                {
                    return NotFound("There are no assets.");
                }
                return Ok(storeAssets);
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

        [HttpGet("read/{serialNumber}")]
        public async Task<IActionResult> ReadBySerialNumber([FromRoute] string serialNumber)
        {
            try
            {
                var storeAssets = await storeAssetServices.ReadBySerialNumber(serialNumber);
                if (storeAssets == null || storeAssets.Any())
                {
                    return NotFound("There is no asset by this Serial Number.");
                }
                return Ok(storeAssets);
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

        // __________________________ Update __________________________
        [HttpPut("update/{assetID}/{serialNumber}")]
        public async Task<IActionResult> Update([FromRoute] int assetID, [FromRoute] int serialNumber, [FromBody] AddOrUpdateStoreAssetsDTO storeAssetsDTO)
        {
            try
            {
                var updatedStoreAsset = await storeAssetServices.Update(storeAssetsDTO, assetID, serialNumber);
                return Ok(updatedStoreAsset);
            }
            catch(KeyNotFoundException ex)
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

        // __________________________ Delete __________________________
        [HttpDelete("delete/{assetID}/{serialNumber}")]
        public async Task<IActionResult> Delete([FromRoute] int assetID, [FromRoute] int serialNumber)
        {
            try
            {
                var result = await storeAssetServices.Delete(assetID, serialNumber);
                return Ok(result);
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
