using DTOs.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Presentation.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public UserManager<IdentityUser> userManager { get; set; }
        public RoleManager<IdentityRole> roleManager { get; set; }
        public IConfiguration configuration { get; set; }
        public IStoreServices storeServices { get; set; }
        public IWarehouseServices warehouseServices { get; set; }
        public ISupplierServices supplierServices { get; set; }
        public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IStoreServices storeServices, IWarehouseServices warehouseServices, ISupplierServices supplierServices)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.storeServices = storeServices;
            this.warehouseServices = warehouseServices;
            this.supplierServices = supplierServices;
        }

        // _____________________________________ Registeration _____________________________________
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDTO registerDTO)
        {
            if (registerDTO == null)
            {
                return BadRequest("There is no data for registration!");
            }
            try
            {
                IdentityUser newUser;
                var role = char.ToUpper(registerDTO.Role[0]) + registerDTO.Role.Substring(1);
                var findUser = await userManager.FindByNameAsync(registerDTO.UserName);
                if (findUser != null)
                    return NotFound("This User already exists!");
                switch (role)
                {
                    case ("Admin"):
                        {
                            newUser = new IdentityUser()
                            {
                                UserName = char.ToUpper(registerDTO.UserName[0]) + registerDTO.UserName.Substring(1),
                            };
                        }
                        break;
                    case ("Supplier"):
                        {
                            var supplier = await supplierServices.ReadByName(registerDTO.UserName);
                            if (supplier == null)
                                return NotFound("There is no supplier by this name.");
                            newUser = new IdentityUser()
                            {
                                UserName = supplier.SupplierName,
                                Id = "SP_" + supplier.SupplierID.ToString(),
                            };
                        }
                        break;
                    case ("Warehouse"):
                        {
                            var warehouse = await warehouseServices.ReadByName(registerDTO.UserName);
                            if (warehouse == null)
                                return NotFound("There is no warehouse by this name.");
                            newUser = new IdentityUser()
                            {
                                UserName = warehouse.WarehouseName,
                                Id = "WH_" + warehouse.WarehouseID.ToString(),
                            };
                        }
                        break;
                    case ("Store"):
                        {
                            var store = await storeServices.ReadByName(registerDTO.UserName);
                            if (store == null)
                                return NotFound("There is no store by this name.");
                            newUser = new IdentityUser()
                            {
                                UserName = store.StoreName,
                                Id = "ST_" + store.StoreID.ToString(),
                            };
                        }
                        break;
                    default:
                        {
                            return NotFound("This role is not avilable.");
                        }
                }
                var result = await userManager.CreateAsync(newUser, registerDTO.Password);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return BadRequest($"User creation failed: {errors}");
                }

                await userManager.AddToRoleAsync(newUser, role);
                return Ok($"The user '{newUser.UserName}' signed up successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // _____________________________________ Login _____________________________________
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO loginDTO)
        {
            if (loginDTO == null)
            {
                return BadRequest("Login data is missing.");
            }

            try
            {
                var user = await userManager.FindByNameAsync(loginDTO.UserName);
                if (user == null)
                    return NotFound("This user was not found!");
                else
                {
                    if (!await userManager.CheckPasswordAsync(user, loginDTO.Password))
                        return BadRequest("The Password is not correct.");
                    else
                    {
                        var userRoles = await userManager.GetRolesAsync(user);
                        var role = userRoles.FirstOrDefault();
                        int id;
                        int.TryParse(user.Id.Substring(3), out id);
                        //var id = user.Id.Substring(3);

                        List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.Id),
                        new Claim(ClaimTypes.Role, role)
                    };

                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
                        var setToken = new JwtSecurityToken(
                            expires: DateTime.Now.AddHours(1),
                            claims: claims,
                            signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256),
                    issuer: configuration["JWT:issuer"],
                        audience: configuration["JWT:audience"]);
                        var token = new JwtSecurityTokenHandler().WriteToken(setToken);

                        var readLoginData = new ReadLoginDataDTO()
                        {
                            Token = "Bearer " + token,
                            Role = role,
                            ID = id,
                        };
                        return Ok(readLoginData);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("validate-token")]
        public async Task<IActionResult> ValidateToken()
        {
            try
            {
                var user = await userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Unauthorized("Invalid token.");
                }

                var userRoles = await userManager.GetRolesAsync(user);
                var role = userRoles.FirstOrDefault();
                int.TryParse(user.Id.Substring(3), out int id);

                var userData = new ReadLoginDataDTO()
                {
                    Token = Request.Headers["Authorization"].ToString(),
                    Role = role,
                    ID = id,
                };

                return Ok(userData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        // _____________________________________ Read _____________________________________
        [HttpGet("readAllUsers")]
        //[Authorize]
        public async Task<IActionResult> ReadAllUsers()
        {
            try
            {
                var users = await userManager.Users.ToListAsync();
                var userDTOs = new List<ReadUserDTO>();

                foreach (var user in users)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    var userDTO = new ReadUserDTO
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Roles = roles
                    };
                    userDTOs.Add(userDTO);
                }

                return Ok(userDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[Authorize]
        [HttpGet("readUser/{username}")]
        public async Task<IActionResult> ReadUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("Username is missing.");
            }

            try
            {
                var user = await userManager.FindByNameAsync(username);
                if (user == null)
                {
                    return NotFound("User not found!");
                }

                var roles = await userManager.GetRolesAsync(user);
                var userDTO = new ReadUserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Roles = roles
                };

                return Ok(userDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}