using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;
using DTOs.DTOs.Stores;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresAssetsController : ControllerBase
    {
        protected IStoreAssetsServices storeAssetServices;
        public StoresAssetsController(IStoreAssetsServices storeAssetServices)
        {
            this.storeAssetServices = storeAssetServices;
        }

        // __________________________ Create __________________________
        [HttpPost("create")]
        public async Task<IActionResult> CreateStoreAsset(AddOrUpdateStoreAssetsDTO addOrUpdateStoreAssetsDTO)
        {
            try
            {
                var result = await storeAssetServices.CreateStoreAsset(addOrUpdateStoreAssetsDTO);
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
                var storeAssets = await storeAssetServices.GetAllStoreAssets();
                return Ok(storeAssets);
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
                var storeAsset = await storeAssetServices.GetOneBySerialNumber(serialNumber);
                return Ok(storeAsset);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // __________________________ Update __________________________
        [HttpPut("/update/{AssetID}/{SerialNumber}")]
        public async Task<IActionResult> Update(int AssetID, int SerialNumber, AddOrUpdateStoreAssetsDTO addOrUpdateStoreAssetsDTO)
        {
            try
            {
                var updatedStoreAsset = await storeAssetServices.UpdateStoreAsset(addOrUpdateStoreAssetsDTO, AssetID, SerialNumber);
                return Ok(updatedStoreAsset);
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
                var result = await storeAssetServices.DeleteStoreAsset(AssetID, SerialNumber);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
