using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;
using DTOs.DTOs.Stores;
using Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [Route("api/store")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        protected IStoreServices storeServices;
        public StoresController(IStoreServices storeServices)
        {
            this.storeServices = storeServices;
        }

        // __________________________ Create __________________________
        [HttpPost("create/data")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] AddStoreDTO storeDTO)
        {
            try
            {
                if (storeDTO == null)
                {
                    return BadRequest("Invalid input data.");
                }
                var createStore = await storeServices.CreateByData(storeDTO);
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
        [HttpPost("create/geojson")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] AddStoreGeoJsonDTO storeDTO)
        {
            try
            {
                if (storeDTO == null)
                {
                    return BadRequest("Invalid input data.");
                }
                var createStore = await storeServices.CreateByGeoJSON(storeDTO);
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
        [HttpGet("read/json")]
        [Authorize]
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
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("read/geojson")]
        //[Authorize]
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
                var store = await storeServices.ReadByID(id);
                if (store == null)
                {
                    return NotFound("There is no store by this ID.");
                }
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
        [HttpGet("read/geojson/{id}")]
        [Authorize]
        public async Task<IActionResult> ReadStoreAsGeoJson(int id)
        {
            try
            {
                var store = await storeServices.ReadByID(id);
                if (store == null)
                {
                    return NotFound("There is no store by this ID.");
                }
                else
                {
                    var storeGeoJson = await storeServices.ReadStoreAsGeoJson(id);
                    return Ok(storeGeoJson);
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

                var stores = await storeServices.Search(name, address);
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
        [HttpPut("update/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStoreDTO updateStoreDTO)
        {
            try
            {
                var store = await storeServices.Update(updateStoreDTO, id);
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
                var result = await storeServices.Delete(id);
                return Ok("The store was deleted successfully.");
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
