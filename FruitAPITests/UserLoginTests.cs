using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FruitAPI.Models;
using System.Text;
using System.Net;public class UsersControllerTests
{
    private readonly Mock<AppDbContext> _mockContext;
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        _mockContext = new Mock<AppDbContext>();
        _controller = new UsersController(_mockContext.Object);
    }

    [Fact]
    public async Task RegisterUser_ValidData_ReturnsOkResult()
    {
        // Arrange
        var request = new LoginRequest { Email = "test@example.com", Password = "password123" };
        
        // Act
        var result = await _controller.RegisterUser(request);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    // Additional tests for invalid data, login method, etc.
}
