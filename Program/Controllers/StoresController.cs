using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;
using DTOs.DTOs.Stores;

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
        public async Task<IActionResult> Create([FromBody] AddOrUpdateStoreDTO storeDTO)
        {
            if (storeDTO == null)
            {
                return BadRequest("Invalid input data.");
            }
            try
            {
                var createStore = await storeServices.Create(storeDTO);
                if (createStore)
                {
                    return Ok("The store was created successfully.");
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
                var stores = await storeServices.ReadAll();
                if (stores == null || !stores.Any())
                {
                    return NotFound("There are no stores.");
                }
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
        [HttpGet("readAllStoresAsGeoJson")]
        public async Task<IActionResult> ReadAllStoresAsGeoJson()
        {
            try
            {
                var storesGeoJson = await storeServices.ReadAllStoresAsGeoJson();
                if (storesGeoJson == null || !storesGeoJson.Any())
                {
                    return NotFound("There are no stores.");
                }
                return Ok(storesGeoJson);
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
                var store = await storeServices.ReadByID(ID);
                if (store == null)
                {
                    return NotFound("There is no store by this ID.");
                }
                return Ok(store);
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
        [HttpGet("readStoreAsGeoJson/{id}")]
        public async Task<IActionResult> ReadStoreAsGeoJson(int id)
        {
            try
            {
                var storeGeoJson = await storeServices.ReadStoreAsGeoJson(id);
                if (storeGeoJson == null)
                {
                    return NotFound();
                }
                return Ok(storeGeoJson);
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
                var stores = await storeServices.SearchByName(name);
                if (stores == null || !stores.Any())
                {
                    return NotFound("There are no stores.");
                }
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

        [HttpGet("searchByAddress/{address}")]
        public async Task<IActionResult> SearchByAddress([FromRoute] string address)
        {
            try
            {
                var stores = await storeServices.SearchByAddress(address);
                if (stores == null || !stores.Any())
                {
                    return NotFound("There are no stores.");
                }
                return Ok(stores);
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
        public async Task<IActionResult> Update([FromRoute] int ID, [FromBody] AddOrUpdateStoreDTO updateStoreDTO)
        {
            try
            {
                var store = await storeServices.Update(updateStoreDTO, ID);
                return Ok(store);
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
                var result = await storeServices.Delete(ID);
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
