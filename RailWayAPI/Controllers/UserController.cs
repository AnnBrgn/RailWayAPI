using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RailWayAPI.ClassesDTO;
using RailWayAPI.Models;
using RailWayAPI.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RailWayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly RailWaySystemContext _context;
        private readonly IConfiguration _configuration;
        public UserController(RailWaySystemContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login([FromBody] UserRegistrationDTO registrationDto)
        {
            Console.WriteLine(registrationDto.Login + " " + registrationDto.Password);
            if (string.IsNullOrEmpty(registrationDto.Login) || string.IsNullOrEmpty(registrationDto.Password))
            {
                return BadRequest("Login and password are required.");
            }

            User user = await _context.Users.FirstOrDefaultAsync(s => s.Login == registrationDto.Login);

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(registrationDto.Password, user.Password);

            if (!isPasswordValid)
            {
                return Unauthorized("Invalid password.");
            }

            if (user != default)
            {
                if(user.Token == null || user.TokenExpierTime >= DateTime.UtcNow)
                {
                    var token = GenerateJwtToken(user);
                    user.Token = token;
                    user.TokenExpierTime = DateTime.UtcNow + TimeSpan.FromDays(7);
                    _context.Entry(user).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                Console.WriteLine(user.Token);
                return Ok(user.ToDto());
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDto)
        {
            if (string.IsNullOrEmpty(userDto.Login) || string.IsNullOrEmpty(userDto.Password))
            {
                return BadRequest("Login and password are required.");
            }

            // Hash the password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            // Create a new user
            var newUser = new User
            {
                Login = userDto.Login,
                Password = hashedPassword, // Store the hashed password
                Birthday =userDto.Birthday,
                Email = userDto.Email,
                Gender = userDto.Gender,
                Lastname = userDto.Lastname,
                Name = userDto.Name,
                NumberPassport = userDto.NumberPassport,
                NumberPhone = userDto.NumberPhone,
                Patronymic = userDto.Patronymic,
                SeriesPassport = userDto.SeriesPassport
            };

            await _context.Users.AddAsync(newUser);

            await _context.SaveChangesAsync();

            return Ok(newUser.ToDto());
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7), // Token expiration time
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
