using Core.BaseMessages;
using DataAcces.SqlServerDbContext;
using Entities.Concrete.TableModels.Membership;
using Entities.Dtos.Membership;
using Entities.TableModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Product.Api.Web._1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        // Route For Seeding my roles to DB
        [HttpPost]
        [Route("seed-roles")]
        public async Task<IActionResult> SeedRoles()
        {
            bool isOwnerRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.OWNER);
            bool isAdminRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.ADMIN);
            bool isUserRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.USER);

            if (isOwnerRoleExists && isAdminRoleExists && isUserRoleExists)
                return Ok("Roles Seeding is Already Done");

            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.USER));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.ADMIN));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.OWNER));

            return Ok("Role Seeding Done Successfully");
        }

        // Route -> Register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var existingUserByEmail = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUserByEmail != null)
            {
                return BadRequest("Email already exists");
            }

            var newUser = new ApplicationUser
            {
                FirstName = registerDto.FirstName,
                UserName = registerDto.UserName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                Gender = registerDto.Gender,
                Password = registerDto.Password,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await _userManager.CreateAsync(newUser, registerDto.Password);

            if (result.Succeeded)
            {
                //if (registerDto.Photo != null)
                //{
                //    var photoPath = await SavePhotoAsync(registerDto.Photo); 
                //}
                await _userManager.AddToRoleAsync(newUser, StaticUserRoles.USER);

                return Ok("User created successfully");
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest($"Failed to create user: {errors}");
            }
            // async Task<string> SavePhotoAsync(IFormFile photo)
            //{
            //    if (photo == null || photo.Length == 0)
            //        return null;

            //    //var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
            //    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            //    if (!Directory.Exists(uploadsFolder))
            //        Directory.CreateDirectory(uploadsFolder);

            //    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
            //    var filePath = Path.Combine(uploadsFolder, fileName);

            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        await photo.CopyToAsync(stream);
            //    }

            //    return "/Images/" + fileName; 
            //}
        }

        // Route -> Login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            ////var user = await _userManager.FindByEmailAsync(loginDto.Email);
            //var user = await _userManager.Users
            //.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            //if (user == null)
            //{
            //    return Unauthorized("Invalid email or password");
            //}

            //var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            //if (!isPasswordCorrect)
            //{
            //    return Unauthorized("Invalid email or password");
            //}

            //var userRoles = await _userManager.GetRolesAsync(user);

            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.NameIdentifier, user.Id),
            //    new Claim(ClaimTypes.Name, user.UserName),
            //    new Claim("JWTID", Guid.NewGuid().ToString())
            //};

            //foreach (var userRole in userRoles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, userRole));
            //}

            //var token = GenerateNewJsonWebToken(claims);

            //return Ok(new { Token = token });


            var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!isPasswordCorrect)
            {
                return Unauthorized("Invalid email or password");
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            int jti = GenerateIntegerJti();
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("Gender", user.Gender.ToString()),
            new Claim("FullName", $"{user.FirstName} {user.LastName}"),
             new Claim("JWTID", jti.ToString()),
            new Claim("JWTID", Guid.NewGuid().ToString())
            };

             int GenerateIntegerJti()
            {
                
                return (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            }

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GenerateNewJsonWebToken(claims);

            return Ok(new { Token = token });
        }

        private string GenerateNewJsonWebToken(List<Claim> claims)
        {
            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var tokenObject = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(1),
                    claims: claims,
                    signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
                );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);

            return token;
        }



        // Route -> make user -> admin
        [HttpPost]
        [Route("make-admin")]
        public async Task<IActionResult> MakeAdmin([FromBody] UpdatePermissionDto updatePermissionDto)
        {

            //var user = await _userManager.FindByEmailAsync(updatePermissionDto.Email);
            var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Email == updatePermissionDto.Email);

            if (user is null)
                return BadRequest("Invalid Email!!!!!!!!");

            await _userManager.AddToRoleAsync(user, StaticUserRoles.ADMIN);

            return Ok("User is now an ADMIN");
        }

        // Route -> make user -> owner
        [HttpPost]
        [Route("make-owner")]
        public async Task<IActionResult> MakeOwner([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            //var user = await _userManager.FindByEmailAsync(updatePermissionDto.Email);
            var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Email == updatePermissionDto.Email);


            if (user is null)
                return BadRequest("Invalid User name!!!!!!!!");

            await _userManager.AddToRoleAsync(user, StaticUserRoles.OWNER);

            return Ok("User is now an Owner");
        }

        public async Task<T> RetryHttpRequest<T>(Func<Task<T>> requestFunc, int maxRetryCount = 3)
        {
            int retryCount = 0;
            while (true)
            {
                try
                {
                    return await requestFunc();
                }
                catch (HttpRequestException ex) when (retryCount < maxRetryCount)
                {

                    retryCount++;
                    await Task.Delay(TimeSpan.FromSeconds(5));
                }
            }
        }
    }
}
