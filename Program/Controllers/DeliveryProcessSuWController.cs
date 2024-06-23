using DTOs.DTOs.DeliveryProcesses;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryProcessSuWController : ControllerBase
    {
        protected IDeliveryProcessSuWServices deliveryProcessSuWServices;
        public DeliveryProcessSuWController(IDeliveryProcessSuWServices deliveryProcessSuWServices)
        {
            this.deliveryProcessSuWServices = deliveryProcessSuWServices;
        }

        // __________________________ Create __________________________
        [HttpPost("create/{supplierID}")]
        public async Task<IActionResult> Create(AddDeliveryProcessSuWDTO addDeliveryProcessSuWDTO, int supplierID)
        {
            if (addDeliveryProcessSuWDTO == null || supplierID <= 0)
            {
                return BadRequest("Invalid input data.");
            }
            try
            {
                var result = await deliveryProcessSuWServices.Create(addDeliveryProcessSuWDTO, supplierID); if (result)
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
        [HttpGet("readAll")]
        public async Task<IActionResult> ReadAll()
        {
            try
            {
                var processes = await deliveryProcessSuWServices.ReadAll();
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
        [HttpGet("readByID/{processID}")]
        public async Task<IActionResult> ReadByID(int processID)
        {
            try
            {
                var process = await deliveryProcessSuWServices.ReadByID(processID);
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
        [HttpGet("searchBySupplier/{supplierID}")]
        public async Task<IActionResult> SearchBySupplier(int supplierID)
        {
            try
            {
                var processes = await deliveryProcessSuWServices.SearchBySupplier(supplierID);
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
        [HttpGet("searchByDate/{date}")]
        public async Task<IActionResult> SearchByDate(DateTime date)
        {
            try
            {
                var processes = await deliveryProcessSuWServices.SearchByDate(date);
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
        [HttpDelete("delete/{processID}")]
        public async Task<IActionResult> Delete(int processID)
        {
            try
            {
                var result = await deliveryProcessSuWServices.DeleteProcess(processID);
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
