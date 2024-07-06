using DTOs.DTOs.DeliveryProcesses;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;
using System.Security.Claims;

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

        [HttpGet("read/{storeID}")]
        public async Task<IActionResult> ReadByStore([FromRoute] int storeID)
        {
            try
            {
                var processes = await storeProcessServices.ReadByStore(storeID);
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
        public async Task<IActionResult> UpdateDelivering([FromRoute] int processID, [FromRoute] int storeID)
        {
            try
            {
                var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (userRole == null)
                {
                    return Unauthorized("User role not found.");
                }

                var updatedProcess = await storeProcessServices.Update(processID, storeID, userRole);
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