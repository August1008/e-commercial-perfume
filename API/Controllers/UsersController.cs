using Core.Entities;
using Core.Models;
using Core.Models.RequestModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        public UsersController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreationModel user)
        {
            var newUser = new AppUser
            {
                Email = user.Email,
                UserName = string.IsNullOrEmpty(user.UserName) ? user.Email : user.UserName,
                Firstname = user.FirstName,
                Lastname = user.LastName,
            };
            var result = await _userManager.CreateAsync(newUser, user.Password);
            if (result.Succeeded)
                return Ok("User Created Successfully");

            return BadRequest(result.Errors);
        }

        [HttpGet("info")]
        public async Task<IActionResult> GetUserInfo()
        {
            if (User.Identity?.IsAuthenticated == false) return NoContent();
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == User.FindFirstValue(ClaimTypes.Email));
            if (user != null)
            {
                return Ok(new UserModel
                {
                    Email = user?.Email,
                    UserName = user?.UserName,
                    FirstName = user?.Firstname,
                    LastName = user?.Lastname
                });
            }
            return Unauthorized();
        }
    }
}
