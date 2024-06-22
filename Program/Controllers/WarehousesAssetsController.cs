using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;
using DTOs.DTOs.Warehouses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesAssetsController : ControllerBase
    {
        protected IWarehouseAssetsServices warehouseAssetsServices;
        public WarehousesAssetsController(IWarehouseAssetsServices warehouseAssetServices)
        {
            this.warehouseAssetsServices = warehouseAssetServices;
        }

        // __________________________ Create __________________________
        [HttpPost("create")]
        public async Task<IActionResult> CreateWarehouseAsset(AddOrUpdateWarehouseAssetsDTO addOrUpdateWarehouseAssetsDTO)
        {
            try
            {
                var result = await warehouseAssetsServices.CreateWarehouseAsset(addOrUpdateWarehouseAssetsDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // __________________________ Read __________________________
        [HttpGet("/readAll")]
        public async Task<IActionResult> ReadAll()
        {
            try
            {
                var warehouseAssets = await warehouseAssetsServices.GetAllWarehouseAssets();
                return Ok(warehouseAssets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("/readOne/{serialNumber}")]
        public async Task<IActionResult> ReadOne(string serialNumber)
        {
            try
            {
                var warehouseAsset = await warehouseAssetsServices.GetOneBySerialNumber(serialNumber);
                return Ok(warehouseAsset);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // __________________________ Update __________________________
        [HttpPut("/update/{AssetID}/{SerialNumber}")]
        public async Task<IActionResult> Update(int AssetID, int SerialNumber, AddOrUpdateWarehouseAssetsDTO addOrUpdateWarehouseAssetsDTO)
        {
            try
            {
                var updatedWarehouseAsset = await warehouseAssetsServices.UpdateWarehouseAsset(addOrUpdateWarehouseAssetsDTO, AssetID, SerialNumber);
                return Ok(updatedWarehouseAsset);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // __________________________ Delete __________________________
        [HttpDelete("/delete/{AssetID}/{SerialNumber}")]
        public async Task<IActionResult> Delete(int AssetID, int SerialNumber)
        {
            try
            {
                var result = await warehouseAssetsServices.DeleteWarehouseAsset(AssetID, SerialNumber);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
