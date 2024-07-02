using DTOs.DTOs.Roles;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> ReadAll()
        {
            var roles = await roleManager.Roles.ToListAsync();
            return Ok(roles);
        }

        // _____________________________________ Add a new Role _____________________________________
        [HttpPost("create")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(AddNewRoleDTO roleDTO)
        {
            roleDTO.Role = char.ToUpper(roleDTO.Role[0]) + roleDTO.Role.Substring(1).ToLower();
            var getRole = await roleManager.FindByNameAsync(roleDTO.Role);
            if (getRole == null)
            {
                IdentityRole newRole = new IdentityRole()
                {
                    Name = roleDTO.Role
                };
                await roleManager.CreateAsync(newRole);
                return Ok(newRole);
            }
            else
                return BadRequest("This role already exists!");
        }
    }
}
