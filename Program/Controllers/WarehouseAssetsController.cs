using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;
using DTOs.DTOs.Warehouses;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [Route("api/warehouse/assets")]
    [ApiController]
    public class WarehouseAssetsController : ControllerBase
    {
        protected IWarehouseAssetsServices warehouseAssetServices;
        private readonly IAuthorizationService authorizationService;

        public WarehouseAssetsController(IWarehouseAssetsServices warehouseAssetServices, IAuthorizationService authorizationService)
        {
            this.warehouseAssetServices = warehouseAssetServices;
            this.authorizationService = authorizationService;
        }

        // __________________________ Create __________________________
        [HttpPost("create/{warehouseID}")]
        public async Task<IActionResult> CreateWarehouseAsset([FromRoute] int warehouseID, [FromBody] AddWarehouseAssetsDTO warehouseAssetsDTO)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, warehouseID, "WarehousePolicy");
                if (!authorizationResult.Succeeded)
                {
                    return Forbid();
                }
                if (warehouseAssetsDTO == null)
                {
                    return BadRequest("Invalid input data.");
                }
                var warehouseAsset = await warehouseAssetServices.Create(warehouseID, warehouseAssetsDTO);
                if (warehouseAsset)
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

        [HttpGet("read/{warehouseID}")]
        public async Task<IActionResult> ReadByWarehouse([FromRoute] int warehouseID)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, warehouseID, "WarehousePolicy");
                if (!authorizationResult.Succeeded)
                {
                    return Forbid();
                }
                var warehouseAssets = await warehouseAssetServices.ReadByWarehouse(warehouseID);
                if (warehouseAssets == null || !warehouseAssets.Any())
                {
                    return NotFound("There are no assets.");
                }
                return Ok(warehouseAssets);
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
        [HttpGet("search/{warehouseID}")]
        public async Task<IActionResult> Search([FromRoute] int warehouseID, [FromQuery] string? warehouseName, [FromQuery] string? serialNumber)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, warehouseID, "WarehousePolicy");
                if (!authorizationResult.Succeeded)
                {
                    return Forbid();
                }
                if (string.IsNullOrWhiteSpace(warehouseName) && string.IsNullOrWhiteSpace(serialNumber))
                {
                    return BadRequest("Either 'name' or 'SerialNum' must be provided.");
                }
                var warehouseAssets = await warehouseAssetServices.Search(warehouseID, warehouseName, serialNumber);
                if (warehouseAssets == null || !warehouseAssets.Any())
                {
                    return NotFound("There are no assets.");
                }
                return Ok(warehouseAssets);
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
        [HttpPut("update/{warehouseID}")]
        public async Task<IActionResult> Update([FromRoute] int warehouseID, [FromQuery] int assetID, [FromQuery] string serialNumber, [FromBody] UpdateWarehouseAssetsDTO warehouseAssetsDTO)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, warehouseID, "WarehousePolicy");
                if (!authorizationResult.Succeeded)
                {
                    return Forbid();
                }
                var updatedWarehouseAsset = await warehouseAssetServices.Update(warehouseAssetsDTO, warehouseID, assetID, serialNumber);
                return Ok(updatedWarehouseAsset);
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
        [HttpDelete("delete/{warehouseID}")]
        public async Task<IActionResult> Delete([FromRoute] int warehouseID, [FromQuery] int assetID, [FromQuery] string serialNumber)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, warehouseID, "WarehousePolicy");
                if (!authorizationResult.Succeeded)
                {
                    return Forbid();
                }
                var result = await warehouseAssetServices.Delete(warehouseID, assetID, serialNumber);
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
