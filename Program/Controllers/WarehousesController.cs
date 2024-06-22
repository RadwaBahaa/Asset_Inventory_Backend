using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;
using DTOs.DTOs.Warehouses;

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
        public async Task<IActionResult> Create([FromBody] AddOrUpdateWarehouseDTO warehouseDTO)
        {
            if (warehouseDTO == null)
            {
                return BadRequest("Invalid input data.");
            }
            try
            {
                var createWarehouse = await warehouseServices.Create(warehouseDTO);
                if (createWarehouse)
                {
                    return Ok("The warehouse was created successfully.");
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
        [HttpGet("/readAll")]
        public async Task<IActionResult> ReadAll()
        {
            try
            {
                var warehouses = await warehouseServices.ReadAll();
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

        [HttpGet("/readByID/{ID:int}")]
        public async Task<IActionResult> ReadByID([FromRoute] int ID)
        {
            try
            {
                var warehouse = await warehouseServices.ReadByID(ID);
                return Ok(warehouse);
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

        // __________________________ Search __________________________
        [HttpGet("/searchByName/{name:string}")]
        public async Task<IActionResult> SearchByName([FromRoute] string name)
        {
            try
            {
                var warehouses = await warehouseServices.SearchByName(name);
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

        [HttpGet("/searchByAddress/{address:string}")]
        public async Task<IActionResult> SearchByAddress([FromRoute] string address)
        {
            try
            {
                var warehouses = await warehouseServices.SearchByAddress(address);
                return Ok(warehouses);
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
        [HttpPut("/update/{ID:int}")]
        public async Task<IActionResult> Update([FromRoute] int ID, [FromBody] AddOrUpdateWarehouseDTO updateWarehouseDTO)
        {
            try
            {
                var warehouse = await warehouseServices.Update(updateWarehouseDTO, ID);
                return Ok(warehouse);
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
        [HttpDelete("/delete/{ID:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await warehouseServices.Delete(ID);
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
