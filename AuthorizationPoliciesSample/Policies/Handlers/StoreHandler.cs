using AuthorizationPoliciesSample.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AuthorizationPoliciesSample.Policies.Handlers
{
    public class StoreHandler : AuthorizationHandler<StoreRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, StoreRequirement requirement)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userId?.Substring(3), out int id);
            var role = context.User.FindFirst(ClaimTypes.Role)?.Value;
            var storeID = context.Resource as int?; // Retrieve storeID from the resource

            // Logging claims and resource values for debugging
            Console.WriteLine($"UserId: {userId}, ID: {id}, Role: {role}, StoreId: {storeID}");

            if (role == "Admin")
            {
                context.Succeed(requirement);
            }
            else if (role == "Store" && storeID == id)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
