﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Services.Services.Interface;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/warehouse/process")]
    [ApiController]
    public class WarehouseProcessController : ControllerBase
    {
        protected IWarehouseProcessServices warehouseProcessServices;
        private readonly IAuthorizationService authorizationService;
        public WarehouseProcessController(IWarehouseProcessServices warehouseProcessServices, IAuthorizationService authorizationService)
        {
            this.warehouseProcessServices = warehouseProcessServices;
            this.authorizationService = authorizationService;
        }

        // __________________________ Read __________________________
        [HttpGet("read/{warehouseID}")]
        public async Task<IActionResult> ReadByWarehouse([FromRoute] int warehouseID)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, warehouseID, "WarehousePolicy");
                if (!authorizationResult.Succeeded)
                {
                    return Forbid();
                }
                var processes = await warehouseProcessServices.ReadByWarehouse(warehouseID);
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

        [HttpGet("read/{processID}/{warehouseID}")]
        public async Task<IActionResult> ReadOne([FromRoute] int processID, [FromRoute] int warehouseID)
        {
            try
            {
                var authorizationResult = await authorizationService.AuthorizeAsync(User, warehouseID, "WarehousePolicy");
                if (!authorizationResult.Succeeded)
                {
                    return Forbid();
                }
                var process = await warehouseProcessServices.ReadByID(processID, warehouseID);
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

        // __________________________ Update __________________________
        [HttpPut("update/{processID}/{warehouseID}")]
        public async Task<IActionResult> Update([FromRoute] int processID, [FromRoute] int warehouseID)
        {
            try
            {
                var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (userRole == null)
                {
                    return Forbid("User role not found.");
                }

                var updatedProcess = await warehouseProcessServices.Update(processID, warehouseID, userRole);
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
