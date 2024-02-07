using FruitAPI.Models;
using FruitAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FruitAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserInfoController : ControllerBase
    {
        private readonly UserProfileService _userService;

        public UserInfoController(UserProfileService userService)
        {
            _userService = userService;
        }

        // POST: api/user
        [HttpPost]
        public async Task<ActionResult<UserInfo>> CreateUser([FromBody] UserInfo userInfo)
        {
            var createdUser = await _userService.CreateUserInfoAsync(userInfo);
            return CreatedAtAction(nameof(GetUser), new { email = createdUser.Email }, createdUser);
        }

        // GET: api/user/{email}
        [HttpGet("{email}")]
        public async Task<ActionResult<UserInfo>> GetUser(string email)
        {
            var user = await _userService.GetUserInfoByIdAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        // GET: api/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetAllUsers()
        {
            var users = await _userService.GetAllUserInfoAsync();
            return Ok(users);
        }

        // PUT: api/user
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserInfo userInfo)
        {
            await _userService.UpdateUserInfoAsync(userInfo);
            return NoContent();
        }

        // DELETE: api/user/{email}
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            await _userService.DeleteUserInfoAsync(email);
            return NoContent();
        }
        
    }
}
