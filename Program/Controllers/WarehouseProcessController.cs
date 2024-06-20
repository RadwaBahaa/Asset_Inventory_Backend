using DTOs.DTOs.DeliveryProcesses;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseProcessController : ControllerBase
    {
        private IWarehouseProcessServices warehouseProcessServices;

        public WarehouseProcessController(IWarehouseProcessServices warehouseProcessServices)
        {
            this.warehouseProcessServices = warehouseProcessServices;
        }

        // __________________________ Read __________________________
        [HttpGet("/readAll")]
        public async Task<ActionResult> ReadAll()
        {
            try
            {
                var processes = await warehouseProcessServices.ReadAllProcess();
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
        [HttpGet("/readOne/{processID}/{warehouseID}")]
        public async Task<ActionResult<ReadWarehouseProcessDTO>> ReadOne(int processID, int warehouseID)
        {
            try
            {
                var process = await warehouseProcessServices.ReadOneProcess(processID, warehouseID);
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
        [HttpGet("/searchBywarehouse/{warehouseID}")]
        public async Task<ActionResult> SearchBywarehouse(int warehouseID)
        {
            try
            {
                var processes = await warehouseProcessServices.SearchByWarehouse(warehouseID);
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
        [HttpPut("/update/{processID}/{warehouseID}")]
        public async Task<ActionResult> Update(int processID, int warehouseID, UpdateWarehouseProcessDTO warehouseProcessDTO)
        {
            try
            {
                var updatedProcess = await warehouseProcessServices.UpdateProcess(processID, warehouseID, warehouseProcessDTO);
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
