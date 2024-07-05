using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;
using DTOs.DTOs.Stores;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [Route("api/store/assets")]
    [ApiController]
    [Authorize]
    public class StoreAssetsController : ControllerBase
    {
        protected IStoreAssetsServices storeAssetServices;
        private readonly IAuthorizationService authorizationService;

        public StoreAssetsController(IStoreAssetsServices storeAssetServices, IAuthorizationService authorizationService)
        {
            this.storeAssetServices = storeAssetServices;
            this.authorizationService = authorizationService;
        }

        // __________________________ Create __________________________
        [HttpPost("create/{storeID}")]
        public async Task<IActionResult> CreateStoreAsset([FromRoute] int storeID, [FromBody] AddStoreAssetsDTO storeAssetsDTO)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, storeID, "StorePolicy");
                if (!authorizationResult.Succeeded)
                {
                    return Forbid();
                }
                if (storeAssetsDTO == null)
                {
                    return BadRequest("Invalid input data.");
                }
                var storeAsset = await storeAssetServices.Create(storeID, storeAssetsDTO);
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

        [HttpGet("read/{storeID}")]
        public async Task<IActionResult> ReadByStore([FromRoute] int storeID)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, storeID, "StorePolicy");
                if (!authorizationResult.Succeeded)
                {
                    return Forbid();
                }
                var storeAssets = await storeAssetServices.ReadByStore(storeID);
                if (storeAssets == null || !storeAssets.Any())
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
        // __________________________ Search __________________________
        [HttpGet("search/{storeID}")]
        public async Task<IActionResult> Search([FromRoute] int storeID, [FromQuery] string? storeName, [FromQuery] string? serialNumber)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, storeID, "StorePolicy");
                if (!authorizationResult.Succeeded)
                {
                    return Forbid();
                }
                if (string.IsNullOrWhiteSpace(storeName) && string.IsNullOrWhiteSpace(serialNumber))
                {
                    return BadRequest("Either 'name' or 'SerialNum' must be provided.");
                }
                var storeAssets = await storeAssetServices.Search(storeID, storeName, serialNumber);
                if (storeAssets == null || !storeAssets.Any())
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

        // __________________________ Update __________________________
        [HttpPut("update/{storeID}")]
        public async Task<IActionResult> Update([FromRoute] int storeID, [FromQuery] int assetID, [FromQuery] string serialNumber, [FromBody] UpdateStoreAssetsDTO storeAssetsDTO)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, storeID, "StorePolicy");
                if (!authorizationResult.Succeeded)
                {
                    return Forbid();
                }
                var updatedStoreAsset = await storeAssetServices.Update(storeAssetsDTO, storeID, assetID, serialNumber);
                return Ok(updatedStoreAsset);
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

        // __________________________ Delete __________________________
        [HttpDelete("delete/{storeID}")]
        public async Task<IActionResult> Delete([FromRoute] int storeID, [FromQuery] int assetID, [FromQuery] string serialNumber)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, storeID, "StorePolicy");
                if (!authorizationResult.Succeeded)
                {
                    return Forbid();
                }
                var result = await storeAssetServices.Delete(storeID, assetID, serialNumber);
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
