using FruitAPI.Data;
using FruitAPI.Models;
using FruitAPI.Services;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;
using System;
using FruitAPI;

namespace FruitAPITests
{
    public class UserProfileServiceTests
    {
        private readonly AppDbContext dbContext;
        private readonly UserProfileService service;

        public UserProfileServiceTests()
        {
            // Configure the DbContext with In-Memory Database for testing
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            dbContext = new AppDbContext(options);
            service = new UserProfileService(dbContext);
        }

        [Fact]
        public async Task CreateUserAsync_ShouldAddUser()
        {
            var userInfo = new UserInfo 

            { 
                Email = "test1@example.com",
                FirstName = "Test",
                LastName = "User",
                City = "TestCity",
                Address = "TestAddress",
                // Set other required properties as needed
            };

            await service.CreateUserInfoAsync(userInfo);

            var userInDb = await dbContext.UserInfo.FirstOrDefaultAsync(u => u.Email == userInfo.Email);
            Assert.NotNull(userInDb);
            Assert.Equal("test1@example.com", userInDb.Email);
        }

       [Fact]
        public async Task GetUserInfoByEmailAsync_ShouldReturnUser()
        {
            var userInfo = new UserInfo
            {
                Email = "retrieve@example.com",
                FirstName = "Retrieve",
                LastName = "User",
                City = "RetrieveCity",
                Address = "RetrieveAddress"
            };

            await service.CreateUserInfoAsync(userInfo);

            var retrievedUser = await service.GetUserInfoByIdAsync(userInfo.Email);
            Assert.NotNull(retrievedUser);
            Assert.Equal("retrieve@example.com", retrievedUser.Email);
        }

        [Fact]
        public async Task UpdateUserInfoAsync_ShouldUpdateUser()
        {
            var userInfo = new UserInfo
            {
                Email = "update@example.com",
                FirstName = "Initial",
                LastName = "User",
                City = "InitialCity",
                Address = "InitialAddress"
            };

            await service.CreateUserInfoAsync(userInfo);

            userInfo.FirstName = "Updated";
            await service.UpdateUserInfoAsync(userInfo);

            var updatedUser = await dbContext.UserInfo.FirstOrDefaultAsync(u => u.Email == userInfo.Email);
            Assert.NotNull(updatedUser);
            Assert.Equal("Updated", updatedUser.FirstName);
        }

        [Fact]
        public async Task DeleteUserInfoAsync_ShouldRemoveUser()
        {
            var userInfo = new UserInfo
            {
                Email = "delete@example.com",
                FirstName = "Delete",
                LastName = "User",
                City = "DeleteCity",
                Address = "DeleteAddress"
            };

            await service.CreateUserInfoAsync(userInfo);

            await service.DeleteUserInfoAsync(userInfo.Email);

            var deletedUser = await dbContext.UserInfo.FirstOrDefaultAsync(u => u.Email == userInfo.Email);
            Assert.Null(deletedUser);
        }

        [Fact]
        public async Task UpdateNonexistentUser_ShouldThrowDbUpdateConcurrencyException()
        {
            var userInfo = new UserInfo
            {
                Email = "nonexistent@example.com",
                FirstName = "Nonexistent",
                LastName = "User",
                City = "NonexistentCity",
                Address = "NonexistentAddress"
            };

            // Expect an exception to be thrown
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(
                async () => await service.UpdateUserInfoAsync(userInfo));
        }

        public void Dispose()
        {
            // Dispose the DbContext after each test
            dbContext.Dispose();
        }
    }
}
