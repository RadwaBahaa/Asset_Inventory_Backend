using DTOs.DTOs.DeliveryProcesses;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;

namespace Presentation.Controllers
{
    [Route("api/store/process")]
    [ApiController]
    public class StoreProcessController : ControllerBase
    {
        protected IStoreProcessServices storeProcessServices;
        public StoreProcessController(IStoreProcessServices storeProcessServices)
        {
            this.storeProcessServices = storeProcessServices;
        }

        // __________________________ Read __________________________
        [HttpGet("read")]
        public async Task<IActionResult> ReadAll()
        {
            try
            {
                var processes = await storeProcessServices.ReadAll();
                return Ok(processes);
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
        [HttpGet("read/{processID}/{storeID}")]
        public async Task<IActionResult> ReadOne([FromRoute] int processID, [FromRoute] int storeID)
        {
            try
            {
                var process = await storeProcessServices.ReadByID(processID, storeID);
                return Ok(process);
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

        // __________________________ Search __________________________
        [HttpGet("search/{storeID}")]
        public async Task<IActionResult> SearchByStore([FromRoute] int storeID)
        {
            try
            {
                var processes = await storeProcessServices.SearchByStore(storeID);
                return Ok(processes);
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

        // __________________________ Update __________________________
        [HttpPut("update/{processID}/{storeID}")]
        public async Task<IActionResult> Update([FromRoute] int processID, [FromRoute] int storeID, [FromBody] UpdateStoreProcessDTO storeProcessDTO)
        {
            try
            {
                var updatedProcess = await storeProcessServices.Update(processID, storeID, storeProcessDTO);
                return Ok(updatedProcess);
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
