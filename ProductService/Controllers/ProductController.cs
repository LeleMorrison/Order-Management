using Microsoft.AspNetCore.Mvc;
using ProductService.Services;
using Shared.Models;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]  // "api/products"
    public class ProductController : ControllerBase
    {
        private readonly ServiceProduct _service;
        public ProductController(ServiceProduct service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _service.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            var created = await _service.CreateAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (product.Id != 0 && product.Id != id)
                return BadRequest("ID prodotto non coerente.");
            var success = await _service.UpdateAsync(id, product);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
