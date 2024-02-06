using FruitAPI.Data;
using FruitAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using FruitAPI.DTOs;

namespace FruitAPI.Services
{
    public class UserProfileService
    {
        private readonly AppDbContext _context;

        public UserProfileService(AppDbContext context)
        {
            _context = context;
        }

        // Create a new UserInfo
        public async Task<UserInfo> CreateUserInfoAsync(UserInfo userInfo)
        {
            _context.UserInfo.Add(userInfo);
            await _context.SaveChangesAsync();
            return userInfo;
        }

        // Get a single UserInfo by Id
        public async Task<UserInfo> GetUserInfoByIdAsync(string email)
        {
            return await _context.UserInfo.FindAsync(email);
        }

        // Get all UserInfo
        public async Task<List<UserInfo>> GetAllUserInfoAsync()
        {
            return await _context.UserInfo.ToListAsync();
        }

        // Update an existing UserInfo
        public async Task<UserInfo> UpdateUserInfoAsync(UserInfo userInfo)
        {
            _context.UserInfo.Update(userInfo);
            await _context.SaveChangesAsync();
            return userInfo;
        }

        // Delete a UserInfo
        public async Task DeleteUserInfoAsync(string email)
        {
            var userInfo = await GetUserInfoByIdAsync(email);
            if (userInfo != null)
            {
                _context.UserInfo.Remove(userInfo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
