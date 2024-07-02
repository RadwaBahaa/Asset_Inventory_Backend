using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;
using DTOs.DTOs.Warehouses;
using Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [Route("api/warehouse")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        protected IWarehouseServices warehouseServices;
        public WarehousesController(IWarehouseServices warehouseServices)
        {
            this.warehouseServices = warehouseServices;
        }

        // __________________________ Create __________________________
        [HttpPost("create/data")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] AddWarehouseDTO warehouseDTO)
        {
            try
            {
                if (warehouseDTO == null)
                {
                    return BadRequest("Invalid input data.");
                }
                var createWarehouse = await warehouseServices.CreateByData(warehouseDTO);
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
        [HttpPost("create/geojson")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] AddWarehouseGeoJsonDTO warehouseDTO)
        {
            try
            {
                if (warehouseDTO == null)
                {
                    return BadRequest("Invalid input data.");
                }
                var createWarehouse = await warehouseServices.CreateByGeoJSON(warehouseDTO);
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
        [HttpGet("read/json")]
        [Authorize]
        public async Task<IActionResult> ReadAll()
        {
            try
            {
                var warehouses = await warehouseServices.ReadAll();
                if (warehouses == null || !warehouses.Any())
                {
                    return NotFound("There are no warehouses.");
                }
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
        [HttpGet("read/geojson")]
        [Authorize]
        public async Task<IActionResult> ReadAllWarehousesAsGeoJson()
        {
            try
            {
                var warehousesGeoJson = await warehouseServices.ReadAllWarehousesAsGeoJson();
                if (warehousesGeoJson == null || !warehousesGeoJson.Any())
                {
                    return NotFound("There are no warehouses.");
                }
                return Ok(warehousesGeoJson);
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
        [HttpGet("read/json/{id}")]
        [Authorize]
        public async Task<IActionResult> ReadByID([FromRoute] int id)
        {
            try
            {
                var warehouse = await warehouseServices.ReadByID(id);
                if (warehouse == null)
                {
                    return NotFound("There is no warehouse by this ID.");
                }
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
        [HttpGet("read/geojson/{id}")]
        [Authorize]
        public async Task<IActionResult> ReadWarehouseAsGeoJson(int id)
        {
            try
            {
                var warehouse = await warehouseServices.ReadByID(id);
                if (warehouse == null)
                {
                    return NotFound("There is no warehouse by this ID.");
                }
                else
                {
                    var warehouseGeoJson = await warehouseServices.ReadWarehouseAsGeoJson(id);
                    return Ok(warehouseGeoJson);
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

        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> Search([FromQuery] string? name, [FromQuery] string? address)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(address))
                {
                    return BadRequest("Either 'name' or 'address' must be provided.");
                }

                var warehouses = await warehouseServices.Search(name, address);
                if (warehouses == null || !warehouses.Any())
                {
                    return NotFound("There are no warehouses.");
                }
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
        [HttpPut("update/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateWarehouseDTO updateWarehouseDTO)
        {
            try
            {
                var warehouse = await warehouseServices.Update(updateWarehouseDTO, id);
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
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // __________________________ Delete __________________________
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var result = await warehouseServices.Delete(id);
                return Ok("The warehouse was deleted successfully.");
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
