using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using AbySalto.Mid.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AbySalto.Mid.WebApi.Models.UserDto;
using AbySalto.Mid.WebApi.Services.AuthService;

namespace AbySalto.Mid.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public UserController(UserManager<User> userManager, ITokenService tokenService
            )
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Email))
                return BadRequest(new List<string> { "Email is already taken." });

            var applicationUser = new User
            {
                Name = registerDto.Name,
                UserName = registerDto.Email,
                Surname = registerDto.Surname,
                Password = registerDto.Password,
                Email = registerDto.Email,
                Location = registerDto.Location,
                PhoneNumber = registerDto.PhoneNumber,
                Address = registerDto.Address,
            };

            var result = await _userManager.CreateAsync(applicationUser, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description + '\n').ToList();
                return BadRequest(errors); 
            }

            var roleResult = await _userManager.AddToRoleAsync(applicationUser, "User");

            if (!roleResult.Succeeded)
            {
                var errors = roleResult.Errors.Select(e => e.Description).ToList();
                return BadRequest(errors);
            }

            return Ok(new RegisterResponseDto
            {
                Name = applicationUser.Name,
                AccessToken = await _tokenService.GenerateJwtToken(applicationUser),
                Email = applicationUser.Email,
                Surname = applicationUser.Surname,
                PhoneNumber = applicationUser.PhoneNumber
            });
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginDto loginDto)
        {
            var applicationUser = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (applicationUser is null)
                return Unauthorized("Invalid username!");

            var result = await _userManager.CheckPasswordAsync(applicationUser, loginDto.Password);

            if (!result)
                return Unauthorized("Invalid password!");

            return new LoginResponseDto
            {
                Email = applicationUser.Email,
                Name = applicationUser.Name,
                Surname = applicationUser.Surname,
                PhoneNumber = applicationUser.PhoneNumber,
                AccessToken = await _tokenService.GenerateJwtToken(applicationUser),
            };
        }

        [Authorize]
        [HttpGet("current-user-info")]
        public async Task<IActionResult> GetCurrentUserInfo()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var applicationUser = await _userManager.FindByIdAsync(userId);

            if (applicationUser == null)
            {
                return NotFound();
            }

            var currentUserDto = new
            {
                applicationUser.Name,
                applicationUser.Surname,
                applicationUser.Email,
                applicationUser.UserName,
            };

            return Ok(currentUserDto);
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
