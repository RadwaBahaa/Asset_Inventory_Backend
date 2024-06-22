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
    public class SuppliersController : ControllerBase
    {
        protected ISupplierServices supplierServices;
        public SuppliersController(ISupplierServices supplierServices)
        {
            this.supplierServices = supplierServices;
        }

        // __________________________ Create __________________________
        [HttpPost("create")]
        public async Task<IActionResult> CreateSupplier(AddOrUpdateSupplierDTO createSupplierDTO)
        {
            try
            {
                var result = await supplierServices.CreateSupplier(createSupplierDTO);
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
                var suppliers = await supplierServices.GetAllSuppliers();
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

        [HttpGet("/readOne/{id:int}")]
        public async Task<IActionResult> ReadOne(int id)
        {
            try
            {
                var supplier = await supplierServices.GetSupplierByID(id);
                return Ok(supplier);
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
            var suppliers = await supplierServices.SearchByName(name);
            return Ok(suppliers);
        }

        [HttpGet("/searchByAddress/{address}")]
        public async Task<IActionResult> SearchByAddress(string address)
        {
            var suppliers = await supplierServices.SearchByAddress(address);
            return Ok(suppliers);
        }

        // __________________________ Update __________________________
        [HttpPut("/update/{id:int}")]
        public async Task<IActionResult> Update(int id, AddOrUpdateSupplierDTO updateSupplierDTO)
        {
            try
            {
                var supplier = await supplierServices.UpdateSupplier(updateSupplierDTO, id);
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
        }

        // __________________________ Delete __________________________
        [HttpDelete("/delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await supplierServices.DeleteSupplier(id);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
