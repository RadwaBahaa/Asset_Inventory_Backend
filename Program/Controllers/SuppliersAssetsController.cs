using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;
using DTOs.DTOs.Suppliers;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersAssetsController : ControllerBase
    {
        protected ISupplierAssetsServices supplierAssetServices;
        public SuppliersAssetsController(ISupplierAssetsServices supplierAssetServices)
        {
            this.supplierAssetServices = supplierAssetServices;
        }

        // __________________________ Create __________________________
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] AddOrUpdateSupplierAssetsDTO supplierAssetsDTO)
        {
            if (supplierAssetsDTO == null)
            {
                return BadRequest("Invalid input data.");
            }
            try
            {
                var supplierAsset = await supplierAssetServices.Create(supplierAssetsDTO);
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

        // __________________________ Read __________________________
        [HttpGet("readAll")]
        public async Task<IActionResult> ReadAll()
        {
            try
            {
                var supplierAssets = await supplierAssetServices.ReadAll();
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

        [HttpGet("readBySerialNumber/{serialNumber}")]
        public async Task<IActionResult> ReadBySerialNumber([FromRoute] string serialNumber)
        {
            try
            {
                var supplierAsset = await supplierAssetServices.ReadBySerialNumber(serialNumber);
                return Ok(supplierAsset);
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
        public async Task<IActionResult> Update([FromRoute] int assetID, [FromRoute] int serialNumber, [FromBody] AddOrUpdateSupplierAssetsDTO supplierAssetsDTO)
        {
            try
            {
                var updatedSupplierAsset = await supplierAssetServices.Update(supplierAssetsDTO, assetID, serialNumber);
                return Ok(updatedSupplierAsset);
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
                var result = await supplierAssetServices.Delete(assetID, serialNumber);
                return Ok(result);
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
