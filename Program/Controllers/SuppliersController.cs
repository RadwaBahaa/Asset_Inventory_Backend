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
        [HttpGet("/readAll")]
        public async Task<IActionResult> ReadAll()
        {
            try
            {
                var suppliers = await supplierServices.ReadAll();
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

        [HttpGet("/readByID/{ID:int}")]
        public async Task<IActionResult> ReadByID([FromRoute] int ID)
        {
            try
            {
                var supplier = await supplierServices.ReadByID(ID);
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

        // __________________________ Search __________________________
        [HttpGet("/searchByName/{name:string}")]
        public async Task<IActionResult> SearchByName([FromRoute] string name)
        {
            try
            {
                var suppliers = await supplierServices.SearchByName(name);
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

        [HttpGet("/searchByAddress/{address:string}")]
        public async Task<IActionResult> SearchByAddress([FromRoute] string address)
        {
            try
            {
                var suppliers = await supplierServices.SearchByAddress(address);
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
        [HttpPut("/update/{ID:int}")]
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
        [HttpDelete("/delete/{ID:int}")]
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
