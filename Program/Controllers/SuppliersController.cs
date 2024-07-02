using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;
using DTOs.DTOs.Suppliers;
using Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [Route("api/supplier")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        protected ISupplierServices supplierServices;
        public SuppliersController(ISupplierServices supplierServices)
        {
            this.supplierServices = supplierServices;
        }

        // __________________________ Create __________________________
        [HttpPost("create/data")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] AddSupplierDTO supplierDTO)
        {
            try
            {
                if (supplierDTO == null)
                {
                    return BadRequest("Invalid input data.");
                }
                var createSupplier = await supplierServices.CreateByData(supplierDTO);
                if (createSupplier)
                {
                    return Ok("The supplier was created successfully.");
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
        public async Task<IActionResult> Create([FromBody] AddSupplierGeoJsonDTO supplierDTO)
        {
            try
            {
                if (supplierDTO == null)
                {
                    return BadRequest("Invalid input data.");
                }
                var createSupplier = await supplierServices.CreateByGeoJSON(supplierDTO);
                if (createSupplier)
                {
                    return Ok("The supplier was created successfully.");
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
                var suppliers = await supplierServices.ReadAll();
                if (suppliers == null || !suppliers.Any())
                {
                    return NotFound("There are no suppliers.");
                }
                return Ok(suppliers);
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
        public async Task<IActionResult> ReadAllSuppliersAsGeoJson()
        {
            try
            {
                var suppliersGeoJson = await supplierServices.ReadAllSuppliersAsGeoJson();
                if (suppliersGeoJson == null || !suppliersGeoJson.Any())
                {
                    return NotFound("There are no suppliers.");
                }
                return Ok(suppliersGeoJson);
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
                var supplier = await supplierServices.ReadByID(id);
                if (supplier == null)
                {
                    return NotFound("There is no supplier by this ID.");
                }
                return Ok(supplier);
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
        public async Task<IActionResult> ReadSupplierAsGeoJson(int id)
        {
            try
            {
                var supplier = await supplierServices.ReadByID(id);
                if (supplier == null)
                {
                    return NotFound("There is no supplier by this ID.");
                }
                else
                {
                    var supplierGeoJson = await supplierServices.ReadSupplierAsGeoJson(id);
                    return Ok(supplierGeoJson);
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

                var suppliers = await supplierServices.Search(name, address);
                if (suppliers == null || !suppliers.Any())
                {
                    return NotFound("There are no suppliers.");
                }
                return Ok(suppliers);
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
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSupplierDTO updateSupplierDTO)
        {
            try
            {
                var supplier = await supplierServices.Update(updateSupplierDTO, id);
                return Ok(supplier);
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
                var result = await supplierServices.Delete(id);
                return Ok("The supplier was deleted successfully.");
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
