using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;
using DTOs.DTOs.Suppliers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersAssetsController : ControllerBase
    {
        protected ISupplierAssetsServices supplierAssetsServices;
        public SuppliersAssetsController(ISupplierAssetsServices supplierAssetServices)
        {
            this.supplierAssetsServices = supplierAssetServices;
        }

        // __________________________ Create __________________________
        [HttpPost("create")]
        public async Task<IActionResult> CreateSupplierAsset(AddOrUpdateSupplierAssetsDTO addOrUpdateSupplierAssetsDTO)
        {
            try
            {
                var result = await supplierAssetsServices.CreateSupplierAsset(addOrUpdateSupplierAssetsDTO);
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
                var supplierAssets = await supplierAssetsServices.GetAllSupplierAssets();
                return Ok(supplierAssets);
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
                var supplierAsset = await supplierAssetsServices.GetOneBySerialNumber(serialNumber);
                return Ok(supplierAsset);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // __________________________ Update __________________________
        [HttpPut("/update/{AssetID}/{SerialNumber}")]
        public async Task<IActionResult> Update(int AssetID, int SerialNumber, AddOrUpdateSupplierAssetsDTO addOrUpdateSupplierAssetsDTO)
        {
            try
            {
                var updatedSupplierAsset = await supplierAssetsServices.UpdateSupplierAsset(addOrUpdateSupplierAssetsDTO, AssetID, SerialNumber);
                return Ok(updatedSupplierAsset);
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
                var result = await supplierAssetsServices.DeleteSupplierAsset(AssetID, SerialNumber);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
