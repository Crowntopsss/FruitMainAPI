using Microsoft.AspNetCore.Mvc;
using FruitAPI.Data;
using FruitAPI.Models;
using System.Linq;
using System.Threading.Tasks;
using FruitAPI.DTOs;

namespace FruitAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] LoginRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Email = request.Email
                };
                user.SetPassword(request.Password); // Setting the password using the new method
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return Ok(new { message = "User registered successfully!" });
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            if (user == null)
            {
                return BadRequest(new { message = "Invalid email or password." });
            }

            if (user.VerifyPassword(request.Password))
            {
                return Ok(new { message = "Logged in successfully!" });
            }
            return BadRequest(new { message = "Invalid email or password." });
        }
    }
}