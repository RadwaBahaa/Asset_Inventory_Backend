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
    public class StoresController : ControllerBase
    {
        protected IStoreServices storeServices;
        public StoresController(IStoreServices storeServices)
        {
            this.storeServices = storeServices;
        }

        // __________________________ Create __________________________
        [HttpPost("create")]
        public async Task<IActionResult> CreateStore(AddOrUpdateStoreDTO createStoreDTO)
        {
            try
            {
                var result = await storeServices.CreateStore(createStoreDTO);
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
                var stores = await storeServices.GetAllStores();
                return Ok(stores);
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
                var store = await storeServices.GetStoreByID(id);
                return Ok(store);
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
            var stores = await storeServices.SearchByName(name);
            return Ok(stores);
        }

        [HttpGet("/searchByAddress/{address}")]
        public async Task<IActionResult> SearchByAddress(string address)
        {
            var stores = await storeServices.SearchByAddress(address);
            return Ok(stores);
        }

        // __________________________ Update __________________________
        [HttpPut("/update/{id:int}")]
        public async Task<IActionResult> Update(int id, AddOrUpdateStoreDTO updateStoreDTO)
        {
            try
            {
                var store = await storeServices.UpdateStore(updateStoreDTO, id);
                return Ok(store);
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
                var result = await storeServices.DeleteStore(id);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
