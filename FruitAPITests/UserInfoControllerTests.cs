using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FruitAPI.Models;
using System.Text;
using System.Net;

public class UserInfoControllerTests : IClassFixture<WebApplicationFactory<Program>> 
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public UserInfoControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateUser_ShouldReturnCreatedResponse()
    {
        var userInfo = new UserInfo
        {
            Email = "create@example.com",
            FirstName = "Create",
            LastName = "User",
            City = "CreateCity",
            Address = "CreateAddress"
        };
        var content = new StringContent(JsonConvert.SerializeObject(userInfo), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/api/userinfo", content);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var returnedUser = JsonConvert.DeserializeObject<UserInfo>(await response.Content.ReadAsStringAsync());
        Assert.Equal("create@example.com", returnedUser.Email);
    }

    [Fact]
    public async Task GetUser_ShouldReturnUser()
    {
        var email = "get@example.com";
        var userInfo = new UserInfo { Email = email, FirstName = "Get", LastName = "User", City = "GetCity", Address = "GetAddress" };
        var content = new StringContent(JsonConvert.SerializeObject(userInfo), Encoding.UTF8, "application/json");
        await _client.PostAsync("/api/userinfo", content);

        var response = await _client.GetAsync($"/api/userinfo/{email}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var returnedUser = JsonConvert.DeserializeObject<UserInfo>(await response.Content.ReadAsStringAsync());
        Assert.Equal(email, returnedUser.Email);
    }

    [Fact]
    public async Task UpdateUser_ShouldReturnNoContentResponse()
    {
        var email = "update@example.com";
        var userInfo = new UserInfo { Email = email, FirstName = "Initial", LastName = "User", City = "InitialCity", Address = "InitialAddress" };
        var content = new StringContent(JsonConvert.SerializeObject(userInfo), Encoding.UTF8, "application/json");
        await _client.PostAsync("/api/userinfo", content);

        userInfo.FirstName = "Updated";
        content = new StringContent(JsonConvert.SerializeObject(userInfo), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("/api/userinfo", content);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteUser_ShouldReturnNoContentResponse()
    {
        var email = "delete@example.com";
        var userInfo = new UserInfo { Email = email, FirstName = "Delete", LastName = "User", City = "DeleteCity", Address = "DeleteAddress" };
        var content = new StringContent(JsonConvert.SerializeObject(userInfo), Encoding.UTF8, "application/json");
        await _client.PostAsync("/api/userinfo", content);

        var response = await _client.DeleteAsync($"/api/userinfo/{email}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}
