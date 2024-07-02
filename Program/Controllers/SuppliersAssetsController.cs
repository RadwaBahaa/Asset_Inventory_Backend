using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;
using DTOs.DTOs.Suppliers;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [Route("api/supplier/assets")]
    [ApiController]
    public class SupplierAssetsController : ControllerBase
    {
        protected ISupplierAssetsServices supplierAssetServices;
        private readonly IAuthorizationService authorizationService;

        public SupplierAssetsController(ISupplierAssetsServices supplierAssetServices, IAuthorizationService authorizationService)
        {
            this.supplierAssetServices = supplierAssetServices;
            this.authorizationService = authorizationService;
        }

        // __________________________ Create __________________________
        [HttpPost("create/{supplierID}")]
        public async Task<IActionResult> CreateSupplierAsset([FromRoute] int supplierID, [FromBody] AddSupplierAssetsDTO supplierAssetsDTO)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, supplierID, "SupplierPolicy");
                if (!authorizationResult.Succeeded)
                {
                    return Forbid();
                }
                if (supplierAssetsDTO == null)
                {
                    return BadRequest("Invalid input data.");
                }
                var supplierAsset = await supplierAssetServices.Create(supplierID, supplierAssetsDTO);
                if (supplierAsset)
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

        [HttpGet("read/{supplierID}")]
        public async Task<IActionResult> ReadBySupplier([FromRoute] int supplierID)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, supplierID, "SupplierPolicy");
                if (!authorizationResult.Succeeded)
                {
                    return Forbid();
                }
                var supplierAssets = await supplierAssetServices.ReadBySupplier(supplierID);
                if (supplierAssets == null || !supplierAssets.Any())
                {
                    return NotFound("There are no assets.");
                }
                return Ok(supplierAssets);
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
        [HttpGet("search/{supplierID}")]
        public async Task<IActionResult> Search([FromRoute] int supplierID, [FromQuery] string? supplierName, [FromQuery] string? serialNumber)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, supplierID, "SupplierPolicy");
                if (!authorizationResult.Succeeded)
                {
                    return Forbid();
                }
                if (string.IsNullOrWhiteSpace(supplierName) && string.IsNullOrWhiteSpace(serialNumber))
                {
                    return BadRequest("Either 'name' or 'SerialNum' must be provided.");
                }
                var supplierAssets = await supplierAssetServices.Search(supplierID, supplierName, serialNumber);
                if (supplierAssets == null || !supplierAssets.Any())
                {
                    return NotFound("There are no assets.");
                }
                return Ok(supplierAssets);
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
        [HttpPut("update/{supplierID}")]
        public async Task<IActionResult> Update([FromRoute] int supplierID, [FromQuery] int assetID, [FromQuery] string serialNumber, [FromBody] UpdateSupplierAssetsDTO supplierAssetsDTO)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, supplierID, "SupplierPolicy");
                if (!authorizationResult.Succeeded)
                {
                    return Forbid();
                }
                var updatedSupplierAsset = await supplierAssetServices.Update(supplierAssetsDTO, supplierID, assetID, serialNumber);
                return Ok(updatedSupplierAsset);
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
        [HttpDelete("delete/{supplierID}")]
        public async Task<IActionResult> Delete([FromRoute] int supplierID, [FromQuery] int assetID, [FromQuery] string serialNumber)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, supplierID, "SupplierPolicy");
                if (!authorizationResult.Succeeded)
                {
                    return Forbid();
                }
                var result = await supplierAssetServices.Delete(supplierID, assetID, serialNumber);
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
