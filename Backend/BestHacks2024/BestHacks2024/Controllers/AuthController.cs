using BestHacks2024.Database.Entities;
using BestHacks2024.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BestHacks2024.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserRegistrationDto userRegistrationDto)
        {
            if (userRegistrationDto == null)
                return BadRequest();

            var user = new User
            {
                Email = userRegistrationDto.Email,
                UserName = userRegistrationDto.Nickname
            };
            var result = await _userManager.CreateAsync(user, userRegistrationDto.Password);

            if (!result.Succeeded)
                return BadRequest(new AuthResponseDto { IsAuthSuccessful = false, ErrorMessage = result.Errors.Select(x => x.Description).FirstOrDefault() });

            //await _userManager.AddToRoleAsync(user, "User");
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, userLoginDto.Password))
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.UserName),
                // Add more claims as needed
            }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"], // Add this line
                Audience = _configuration["Jwt:Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var userDtoList = new List<UserDto>();
            _userManager.Users.ToList().ForEach(x => userDtoList.Add(new UserDto { Username = x.UserName }));
            return Ok(userDtoList);
        }
    }
}
