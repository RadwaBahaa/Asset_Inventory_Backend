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
    public class WarehousesController : ControllerBase
    {
        protected IWarehouseServices warehouseServices;
        public WarehousesController(IWarehouseServices warehouseServices)
        {
            this.warehouseServices = warehouseServices;
        }

        // __________________________ Create __________________________
        [HttpPost("create")]
        public async Task<IActionResult> CreateWarehouse(AddOrUpdateWarehouseDTO createWarehouseDTO)
        {
            try
            {
                var result = await warehouseServices.CreateWarehouse(createWarehouseDTO);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // __________________________ Read __________________________
        [HttpGet("/readAll")]
        public async Task<IActionResult> ReadAll()
        {
            try
            {
                var warehouses = await warehouseServices.GetAllWarehouses();
                return Ok(warehouses);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("/readOne/{id:int}")]
        public async Task<IActionResult> ReadOne(int id)
        {
            try
            {
                var warehouse = await warehouseServices.GetWarehouseByID(id);
                return Ok(warehouse);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // __________________________ Search __________________________
        [HttpGet("/searchByName/{name}")]
        public async Task<IActionResult> SearchByName(string name)
        {
            var warehouses = await warehouseServices.SearchByName(name);
            return Ok(warehouses);
        }

        [HttpGet("/searchByAddress/{address}")]
        public async Task<IActionResult> SearchByAddress(string address)
        {
            var warehouses = await warehouseServices.SearchByAddress(address);
            return Ok(warehouses);
        }

        // __________________________ Update __________________________
        [HttpPut("/update/{id:int}")]
        public async Task<IActionResult> Update(int id, AddOrUpdateWarehouseDTO updateWarehouseDTO)
        {
            try
            {
                var warehouse = await warehouseServices.UpdateWarehouse(updateWarehouseDTO, id);
                return Ok(warehouse);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // __________________________ Delete __________________________
        [HttpDelete("/delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await warehouseServices.DeleteWarehouse(id);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
