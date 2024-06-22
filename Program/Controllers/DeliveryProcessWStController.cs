using DTOs.DTOs.DeliveryProcesses;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryProcessWStController : ControllerBase
    {
        protected IDeliveryProcessWStServices deliveryProcessWStServices;
        public DeliveryProcessWStController(IDeliveryProcessWStServices deliveryProcessWStServices)
        {
            this.deliveryProcessWStServices = deliveryProcessWStServices;
        }

        // __________________________ Create __________________________
        [HttpPost("/create/{warehouseID:int}")]
        public async Task<IActionResult> Create(AddDeliveryProcessWStDTO addDeliveryProcessWStDTO, int warehouseID)
        {
            if (addDeliveryProcessWStDTO == null || warehouseID <= 0)
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
        [HttpGet("/readAll")]
        public async Task<IActionResult> ReadAll()
        {
            try
            {
                var processes = await deliveryProcessWStServices.ReadAll();
                return Ok(processes);
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
        [HttpGet("/readByID/{processID:int}")]
        public async Task<IActionResult> ReadByID(int processID)
        {
            try
            {
                var process = await deliveryProcessWStServices.ReadByID(processID);
                return Ok(process);
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
        [HttpGet("/searchByWarehouse/{warehouseID:int}")]
        public async Task<IActionResult> SearchByWarehouse(int warehouseID)
        {
            try
            {
                var processes = await deliveryProcessWStServices.SearchByWarehouse(warehouseID);
                return Ok(processes);
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

        [HttpGet("/searchByDate/{date:datetime}")]
        public async Task<IActionResult> SearchByDate(DateTime date)
        {
            try
            {
                var processes = await deliveryProcessWStServices.SearchByDate(date);
                return Ok(processes);
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
        [HttpDelete("/delete/{processID:int}")]
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
