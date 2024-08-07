﻿using Presentation.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Presentation.Policies.Handlers
{
    public class WarehouseHandler : AuthorizationHandler<WarehouseRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, WarehouseRequirement requirement)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userId?.Substring(3), out int id);
            var role = context.User.FindFirst(ClaimTypes.Role)?.Value;
            var warehouseID = context.Resource as int?; // Retrieve warehouseID from the resource

            if (role == "Admin")
            {
                context.Succeed(requirement);
            }
            else if (role == "Warehouse" && warehouseID == id)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
