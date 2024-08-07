﻿using DTOs.DTOs.DeliveryProcesses;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interface;

namespace Presentation.Controllers
{
    [Route("api/delivery-process/supplier-warehouse")]
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
        public async Task<IActionResult> Create(AddDeliveryProcessSuWDTO deliveryProcessSuWDTO, int supplierID)
        {
            if (deliveryProcessSuWDTO == null || supplierID == 0)
            {
                return BadRequest("Invalid input data.");
            }
            try
            {
                var result = await deliveryProcessSuWServices.Create(deliveryProcessSuWDTO, supplierID);
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
        [HttpGet("readAll")]
        public async Task<IActionResult> ReadAll()
        {
            try
            {
                var processes = await deliveryProcessSuWServices.ReadAll();
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
                var process = await deliveryProcessSuWServices.ReadByID(processID);
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
        [HttpGet("read-by-supplier/{supplierID:int}")]
        public async Task<IActionResult> ReadBySupplier(int supplierID)
        {
            try
            {
                var process = await deliveryProcessSuWServices.ReadBySupplier(supplierID);
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
                var process = await deliveryProcessSuWServices.ReadByWarehouse(warehouseID);
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
                var processes = await deliveryProcessSuWServices.Search(date);
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