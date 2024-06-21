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
        [HttpGet("/readAll")]
        public async Task<IActionResult> ReadAll()
        {
            try
            {
                var processes = await storeProcessServices.ReadAllProcess();
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
        [HttpGet("/readOne/{processID:int}/{storeID:int}")]
        public async Task<IActionResult> ReadOne(int processID, int storeID)
        {
            try
            {
                var process = await storeProcessServices.ReadOneProcess(processID, storeID);
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
        [HttpGet("/searchByStore/{storeID:int}")]
        public async Task<IActionResult> SearchByStore(int storeID)
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
        [HttpPut("/update/{processID:int}/{storeID:int}")]
        public async Task<IActionResult> Update(int processID, int storeID, UpdateStoreProcessDTO storeProcessDTO)
        {
            try
            {
                var updatedProcess = await storeProcessServices.UpdateProcess(processID, storeID, storeProcessDTO);
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
