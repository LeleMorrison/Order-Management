using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OrderManagement.API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersGatewayController : ControllerBase
    {
        // URL base del servizio Ordini (OrdersService)
        private const string OrdersServiceBaseUrl = "http://localhost:5001/api/orders";

        private readonly HttpClient _httpClient;
        public OrdersGatewayController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            // Inoltra la richiesta GET al servizio Ordini
            var response = await _httpClient.GetAsync(OrdersServiceBaseUrl);
            var content = await response.Content.ReadAsStringAsync();
            // Restituisce la risposta (lista di ordini in formato JSON)
            return Content(content, "application/json", System.Text.Encoding.UTF8);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var response = await _httpClient.GetAsync($"{OrdersServiceBaseUrl}/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json", System.Text.Encoding.UTF8);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] object orderData)
        {
            // Inoltra la richiesta POST (creazione ordine)
            var response = await _httpClient.PostAsJsonAsync(OrdersServiceBaseUrl, orderData);
            var content = await response.Content.ReadAsStringAsync();
            return new ContentResult
            {
                Content = content,
                ContentType = "application/json",
                StatusCode = (int)response.StatusCode
            };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] object orderData)
        {
            var response = await _httpClient.PutAsJsonAsync($"{OrdersServiceBaseUrl}/{id}", orderData);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            // Nessun contenuto da restituire in caso di successo (204 NoContent)
            return StatusCode((int)response.StatusCode);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var response = await _httpClient.DeleteAsync($"{OrdersServiceBaseUrl}/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return NotFound();
            return StatusCode((int)response.StatusCode);
        }
    }
}
