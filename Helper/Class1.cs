using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Helper
{
    public static class TestHelpers
    {
        private static readonly HttpClient Client = new HttpClient();

        // Helper function to create a user and return the created user
        public static async Task<UserInfo> CreateUserAsync(WebApplicationFactory<Program> factory, UserInfo userInfo)
        {
            using var client = factory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(userInfo), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/userinfo", content);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            return JsonConvert.DeserializeObject<UserInfo>(await response.Content.ReadAsStringAsync());
        }

        // Helper function to get a user by email
        public static async Task<UserInfo> GetUserByEmailAsync(WebApplicationFactory<Program> factory, string email)
        {
            using var client = factory.CreateClient();
            var response = await client.GetAsync($"/api/userinfo/{email}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            return JsonConvert.DeserializeObject<UserInfo>(await response.Content.ReadAsStringAsync());
        }

        // Helper function to update a user and return the updated user
        public static async Task<UserInfo> UpdateUserAsync(WebApplicationFactory<Program> factory, UserInfo userInfo)
        {
            using var client = factory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(userInfo), Encoding.UTF8, "application/json");

            var response = await client.PutAsync("/api/userinfo", content);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            return userInfo;
        }

        // Helper function to delete a user by email
        public static async Task DeleteUserAsync(WebApplicationFactory<Program> factory, string email)
        {
            using var client = factory.CreateClient();
            var response = await client.DeleteAsync($"/api/userinfo/{email}");
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
