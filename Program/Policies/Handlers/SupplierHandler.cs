using Presentation.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Presentation.Policies.Handlers
{
    public class SupplierHandler : AuthorizationHandler<SupplierRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SupplierRequirement requirement)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userId?.Substring(3), out int id);
            var role = context.User.FindFirst(ClaimTypes.Role)?.Value;
            var supplierID = context.Resource as int?; // Retrieve supplierID from the resource

            // Logging claims and resource values for debugging
            Console.WriteLine($"UserId: {userId}, ID: {id}, Role: {role}, SupplierId: {supplierID}");

            if (role == "Admin")
            {
                context.Succeed(requirement);
            }
            else if (role == "Supplier" && supplierID == id)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
