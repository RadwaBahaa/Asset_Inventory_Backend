using DTOs.DTOs.DeliveryProcesses;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;

namespace Presentation.Controllers
{
    [Route("api/delivery-process/warehouse-store")]
    [ApiController]
    public class DeliveryProcessWStController : ControllerBase
    {
        protected IDeliveryProcessWStServices deliveryProcessWStServices;
        public DeliveryProcessWStController(IDeliveryProcessWStServices deliveryProcessWStServices)
        {
            this.deliveryProcessWStServices = deliveryProcessWStServices;
        }

        // __________________________ Create __________________________
        [HttpPost("create/{warehouseID}")]
        public async Task<IActionResult> Create(AddDeliveryProcessWStDTO addDeliveryProcessWStDTO, int warehouseID)
        {
            if (addDeliveryProcessWStDTO == null || warehouseID ==0)
            {
                return BadRequest("Invalid input data.");
            }
            try
            {
                var result = await deliveryProcessWStServices.Create(addDeliveryProcessWStDTO, warehouseID);
                if (result)
                {
                    return Ok("Delivery process created successfully.");
                }
                else
                {
                    return NotFound("Process not found.");
                }
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

        // __________________________ Read __________________________
        [HttpGet("read")]
        public async Task<IActionResult> ReadAll()
        {
            try
            {
                var processes = await deliveryProcessWStServices.ReadAll();
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
        [HttpGet("read/{processID:int}")]
        public async Task<IActionResult> ReadByID(int processID)
        {
            try
            {
                var process = await deliveryProcessWStServices.ReadByID(processID);
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
        [HttpGet("read-by-warehouse/{warehouseID:int}")]
        public async Task<IActionResult> ReadByWarehouse(int warehouseID)
        {
            try
            {
                var process = await deliveryProcessWStServices.ReadByWarehouse(warehouseID);
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
        [HttpGet("read-by-store/{storeID:int}")]
        public async Task<IActionResult> ReadByStore(int storeID)
        {
            try
            {
                var process = await deliveryProcessWStServices.ReadByStore(storeID);
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
        [HttpGet("search")]
        public async Task<IActionResult> Search(DateTime? date)
        {
            try
            {
                var processes = await deliveryProcessWStServices.Search(date);
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

        // __________________________ Delete __________________________
        [HttpDelete("delete/{processID}")]
        public async Task<IActionResult> Delete(int processID)
        {
            try
            {
                var result = await deliveryProcessWStServices.DeleteProcess(processID);
                if (result)
                {
                    return Ok("Process deleted successfully.");
                }
                else
                {
                    return NotFound("Process not found.");
                }
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