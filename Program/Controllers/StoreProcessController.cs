using DTOs.DTOs.DeliveryProcesses;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreProcessController : ControllerBase
    {
        protected IStoreProcessServices storeProcessServices;
        public StoreProcessController(IStoreProcessServices storeProcessServices)
        {
            this.storeProcessServices = storeProcessServices;
        }

        // __________________________ Read __________________________
        [HttpGet("readAll")]
        public async Task<IActionResult> ReadAll()
        {
            try
            {
                var processes = await storeProcessServices.ReadAll();
                return Ok(processes);
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
        [HttpGet("readByID/{processID}/{storeID}")]
        public async Task<IActionResult> ReadOne([FromRoute] int processID, [FromRoute] int storeID)
        {
            try
            {
                var process = await storeProcessServices.ReadByID(processID, storeID);
                return Ok(process);
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
        [HttpGet("searchByStore/{storeID}")]
        public async Task<IActionResult> SearchByStore([FromRoute] int storeID)
        {
            try
            {
                var processes = await storeProcessServices.SearchByStore(storeID);
                return Ok(processes);
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

        // __________________________ Update __________________________
        [HttpPut("update/{processID}/{storeID}")]
        public async Task<IActionResult> Update([FromRoute] int processID, [FromRoute] int storeID, [FromBody] UpdateStoreProcessDTO storeProcessDTO)
        {
            try
            {
                var updatedProcess = await storeProcessServices.Update(processID, storeID, storeProcessDTO);
                return Ok(updatedProcess);
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
