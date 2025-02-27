using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OrderManagement.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersGatewayController : ControllerBase
    {
        private const string UsersServiceBaseUrl = "http://localhost:5003/api/users";
        private readonly HttpClient _httpClient;
        public UsersGatewayController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _httpClient.GetAsync(UsersServiceBaseUrl);
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var response = await _httpClient.GetAsync($"{UsersServiceBaseUrl}/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] object userData)
        {
            var response = await _httpClient.PostAsJsonAsync(UsersServiceBaseUrl, userData);
            var content = await response.Content.ReadAsStringAsync();
            return new ContentResult
            {
                Content = content,
                ContentType = "application/json",
                StatusCode = (int)response.StatusCode
            };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] object userData)
        {
            var response = await _httpClient.PutAsJsonAsync($"{UsersServiceBaseUrl}/{id}", userData);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            return StatusCode((int)response.StatusCode);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _httpClient.DeleteAsync($"{UsersServiceBaseUrl}/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            return StatusCode((int)response.StatusCode);
        }
    }
}
