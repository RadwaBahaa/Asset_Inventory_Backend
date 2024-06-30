using DTOs.DTOs.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        public RoleManager<IdentityRole> roleManager { get; set; }
        public UserManager<IdentityUser> userManager { get; set; }
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        // _____________________________________ Get All Roles _____________________________________
        [HttpGet("readAll")]
        public async Task<IActionResult> ReadAll()
        {
            var roles = await roleManager.Roles.ToListAsync();
            return Ok(roles);
        }

        // _____________________________________ Add a new Role _____________________________________
        [HttpPost("create")]
        public async Task<IActionResult> Create(AddNewRoleDTO roleDTO)
        {
            roleDTO.RoleName = char.ToUpper(roleDTO.RoleName[0]) + roleDTO.RoleName.Substring(1).ToLower();
            var getRole = await roleManager.FindByNameAsync(roleDTO.RoleName);
            if (getRole == null)
            {
                IdentityRole newRole = new IdentityRole()
                {
                    Name = roleDTO.RoleName
                };
                await roleManager.CreateAsync(newRole);
                return Ok(newRole);
            }
            else
                return BadRequest("This role already exists!");
        }

        // _____________________________________ Add Role to a user _____________________________________
        [HttpPost("addRoleToUser")]
        public async Task<IActionResult> AddRoleToUser(AddRoleToUser roleDTO)
        {
            roleDTO.RoleName = char.ToUpper(roleDTO.RoleName[0]) + roleDTO.RoleName.Substring(1).ToLower();
            var findRole = await roleManager.FindByNameAsync(roleDTO.RoleName);
            if (findRole == null)
                return NotFound("There is no role with this name!");
            var user = await userManager.FindByNameAsync(roleDTO.UserName);
            if (user == null)
                return NotFound("There is no user with this name!");
            else
            {
                var userRoles = await userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    if (role.ToLower() == roleDTO.RoleName.ToLower())
                        return BadRequest($"The user '{user.UserName}' already has this role!");
                }
                await userManager.AddToRoleAsync(user, roleDTO.RoleName);
                return Ok($"The role '{roleDTO.RoleName}' was added to the user '{user.UserName}' successfully.");
            }
        }
    }
}
