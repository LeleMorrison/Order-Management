using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OrderManagement.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsGatewayController : ControllerBase
    {
        private const string ProductsServiceBaseUrl = "http://localhost:5002/api/products";
        private readonly HttpClient _httpClient;
        public ProductsGatewayController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await _httpClient.GetAsync(ProductsServiceBaseUrl);
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var response = await _httpClient.GetAsync($"{ProductsServiceBaseUrl}/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] object productData)
        {
            var response = await _httpClient.PostAsJsonAsync(ProductsServiceBaseUrl, productData);
            var content = await response.Content.ReadAsStringAsync();
            return new ContentResult
            {
                Content = content,
                ContentType = "application/json",
                StatusCode = (int)response.StatusCode
            };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] object productData)
        {
            var response = await _httpClient.PutAsJsonAsync($"{ProductsServiceBaseUrl}/{id}", productData);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            return StatusCode((int)response.StatusCode);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await _httpClient.DeleteAsync($"{ProductsServiceBaseUrl}/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            return StatusCode((int)response.StatusCode);
        }
    }
}
