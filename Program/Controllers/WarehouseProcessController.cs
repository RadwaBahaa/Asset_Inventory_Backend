using DTOs.DTOs.DeliveryProcesses;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseProcessController : ControllerBase
    {
        protected IWarehouseProcessServices warehouseProcessServices;
        public WarehouseProcessController(IWarehouseProcessServices warehouseProcessServices)
        {
            this.warehouseProcessServices = warehouseProcessServices;
        }

        // __________________________ Read __________________________
        [HttpGet("/readAll")]
        public async Task<IActionResult> ReadAll()
        {
            try
            {
                var processes = await warehouseProcessServices.ReadAll();
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
        [HttpGet("/readByID/{processID:int}/{warehouseID:int}")]
        public async Task<IActionResult> ReadOne([FromRoute] int processID, [FromRoute] int warehouseID)
        {
            try
            {
                var process = await warehouseProcessServices.ReadByID(processID, warehouseID);
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
        [HttpGet("/searchBywarehouse/{warehouseID:int}")]
        public async Task<IActionResult> SearchBywarehouse([FromRoute] int warehouseID)
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
        [HttpPut("/update/{processID:int}/{warehouseID:int}")]
        public async Task<IActionResult> Update([FromRoute] int processID, [FromRoute] int warehouseID, [FromBody] UpdateWarehouseProcessDTO warehouseProcessDTO)
        {
            try
            {
                var updatedProcess = await warehouseProcessServices.Update(processID, warehouseID, warehouseProcessDTO);
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
