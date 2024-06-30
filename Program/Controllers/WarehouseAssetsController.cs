using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;
using DTOs.DTOs.Warehouses;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WarehouseAssetsController : ControllerBase
    {
        protected IWarehouseAssetsServices warehouseAssetServices;
        public WarehouseAssetsController(IWarehouseAssetsServices warehouseAssetServices)
        {
            this.warehouseAssetServices = warehouseAssetServices;
        }

        // __________________________ Create __________________________
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] AddOrUpdateWarehouseAssetsDTO warehouseAssetsDTO)
        {
            if (warehouseAssetsDTO == null)
            {
                return BadRequest("Invalid input data.");
            }
            try
            {
                var warehouseAsset = await warehouseAssetServices.Create(warehouseAssetsDTO);
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

        // __________________________ Read __________________________
        [HttpGet("readAll")]
        public async Task<IActionResult> ReadAll()
        {
            try
            {
                var warehouseAssets = await warehouseAssetServices.ReadAll();
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

        [HttpGet("readBySerialNumber/{serialNumber}")]
        public async Task<IActionResult> ReadBySerialNumber([FromRoute] string serialNumber)
        {
            try
            {
                var warehouseAsset = await warehouseAssetServices.ReadBySerialNumber(serialNumber);
                return Ok(warehouseAsset);
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
        public async Task<IActionResult> Update([FromRoute] int assetID, [FromRoute] int serialNumber, [FromBody] AddOrUpdateWarehouseAssetsDTO warehouseAssetsDTO)
        {
            try
            {
                var updatedWarehouseAsset = await warehouseAssetServices.Update(warehouseAssetsDTO, assetID, serialNumber);
                return Ok(updatedWarehouseAsset);
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
                var result = await warehouseAssetServices.Delete(assetID, serialNumber);
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
