using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OrderManagement.API.Controllers
{
    [ApiController]
    [Route("api/addresses")]
    public class AddressesGatewayController : ControllerBase
    {
        private const string AddressServiceBaseUrl = "http://localhost:5004/api/addresses";
        private readonly HttpClient _httpClient;
        public AddressesGatewayController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAddresses()
        {
            var response = await _httpClient.GetAsync(AddressServiceBaseUrl);
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressById(int id)
        {
            var response = await _httpClient.GetAsync($"{AddressServiceBaseUrl}/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] object addressData)
        {
            var response = await _httpClient.PostAsJsonAsync(AddressServiceBaseUrl, addressData);
            var content = await response.Content.ReadAsStringAsync();
            return new ContentResult
            {
                Content = content,
                ContentType = "application/json",
                StatusCode = (int)response.StatusCode
            };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] object addressData)
        {
            var response = await _httpClient.PutAsJsonAsync($"{AddressServiceBaseUrl}/{id}", addressData);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            return StatusCode((int)response.StatusCode);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var response = await _httpClient.DeleteAsync($"{AddressServiceBaseUrl}/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            return StatusCode((int)response.StatusCode);
        }
    }
}
