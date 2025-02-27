using System;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using OrdersService.Services;

namespace OrdersService.Controllers
{

    [ApiController]
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private readonly ServiceOrders _service;
        public OrderController(ServiceOrders service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            var orders = await _service.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetById(int id)
        {
            var order = await _service.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Create(Order newOrder)
        {
            var created = await _service.CreateOrderAsync(newOrder);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteOrderAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
