using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;
using DTOs.DTOs.Suppliers;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        protected ISupplierServices supplierServices;
        public SuppliersController(ISupplierServices supplierServices)
        {
            this.supplierServices = supplierServices;
        }

        // __________________________ Create __________________________
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] AddOrUpdateSupplierDTO supplierDTO)
        {
            if (supplierDTO == null)
            {
                return BadRequest("Invalid input data.");
            }
            try
            {
                var createSupplier = await supplierServices.Create(supplierDTO);
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
        [HttpGet("readAll")]
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
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("readAllSuppliersAsGeoJson")]
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
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("readByID/{ID}")]
        public async Task<IActionResult> ReadByID([FromRoute] int ID)
        {
            try
            {
                var supplier = await supplierServices.ReadByID(ID);
                if (supplier == null)
                {
                    return NotFound("There is no supplier by this ID.");
                }
                return Ok(supplier);
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
        [HttpGet("readSupplierAsGeoJson/{id}")]
        public async Task<IActionResult> ReadSupplierAsGeoJson(int id)
        {
            try
            {
                var supplierGeoJson = await supplierServices.ReadSupplierAsGeoJson(id);
                if (supplierGeoJson == null)
                {
                    return NotFound();
                }
                return Ok(supplierGeoJson);
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
        [HttpGet("searchByName/{name}")]
        public async Task<IActionResult> SearchByName([FromRoute] string name)
        {
            try
            {
                var suppliers = await supplierServices.SearchByName(name);
                if (suppliers == null || !suppliers.Any())
                {
                    return NotFound("There are no suppliers.");
                }
                return Ok(suppliers);
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

        [HttpGet("searchByAddress/{address}")]
        public async Task<IActionResult> SearchByAddress([FromRoute] string address)
        {
            try
            {
                var suppliers = await supplierServices.SearchByAddress(address);
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
        [HttpPut("update/{ID}")]
        public async Task<IActionResult> Update([FromRoute] int ID, [FromBody] AddOrUpdateSupplierDTO updateSupplierDTO)
        {
            try
            {
                var supplier = await supplierServices.Update(updateSupplierDTO, ID);
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

        // __________________________ Delete __________________________
        [HttpDelete("delete/{ID}")]
        public async Task<IActionResult> Delete(int ID)
        {
            try
            {
                var result = await supplierServices.Delete(ID);
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
