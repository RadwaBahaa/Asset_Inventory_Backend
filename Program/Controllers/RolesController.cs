using DTOs.DTOs.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin, StoreManager, WarehouseManager")] // Allow access for both Admin and Manager roles
    public class RolesController : ControllerBase
    {
        public RoleManager<IdentityRole> RoleManager { get; set; }
        public UserManager<User> UserManager { get; set; }
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.RoleManager = roleManager;
            this.UserManager = userManager;
        }

        // _____________________________________ Get All Roles _____________________________________
        [HttpGet]
        public async Task<IActionResult> ReadAll()
        {
            var roles = await RoleManager.Roles.ToListAsync();
            return Ok(roles);
        }

        // _____________________________________ Add a new Role _____________________________________
        [HttpPost]
        public async Task<IActionResult> Create(AddNewRoleDTO roleDTO)
        {
            roleDTO.RoleName = char.ToUpper(roleDTO.RoleName[0]) + roleDTO.RoleName.Substring(1).ToLower();
            var getRole = await RoleManager.FindByNameAsync(roleDTO.RoleName);
            if (getRole == null)
            {
                IdentityRole newRole = new IdentityRole()
                {
                    Name = roleDTO.RoleName
                };
                await RoleManager.CreateAsync(newRole);
                return Ok(newRole);
            }
            else
                return BadRequest("This role already exists !.... ");
        }

        // _____________________________________ Add Role to a user _____________________________________
        [HttpPost]
        public async Task<IActionResult> AddRoleToUser(AddRoleToUser roleDTO)
        {
            roleDTO.RoleName = char.ToUpper(roleDTO.RoleName[0]) + roleDTO.RoleName.Substring(1).ToLower();
            var user = await UserManager.FindByNameAsync(roleDTO.UserName);
            if (user == null)
                return BadRequest("There is no user with this name !....");
            else
            {
                var userRoles = await UserManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    if (role.ToLower() == roleDTO.RoleName.ToLower())
                        return BadRequest($"The user '{user.UserName}' already has this role !....");
                }
                await UserManager.AddToRoleAsync(user, roleDTO.RoleName);
                return Ok($"The role '{roleDTO.RoleName}' was added to the user '{user.UserName}' successfully !....");
            }
        }
    }
}
