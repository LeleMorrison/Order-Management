using AddressService.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace AddressService.Controllers
{
    [ApiController]
    [Route("api/addresses")]  // "api/addresses"
    public class AddresseController : ControllerBase
    {
        private readonly ServiceAddress _service;
        public AddresseController(ServiceAddress service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            var addresses = await _service.GetAllAsync();
            return Ok(addresses);
        }

        [HttpGet("{id}", Name = "GetAddress")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var address = await _service.GetByIdAsync(id);
            if (address == null)
                return NotFound();
            return Ok(address);
        }

        [HttpPost]
        public async Task<ActionResult<Address>> CreateAddress(Address address)
        {
            var created = await _service.CreateAsync(address);
            return CreatedAtAction(nameof(GetAddress), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(int id, Address address)
        {
            if (address.Id != 0 && address.Id != id)
                return BadRequest("ID indirizzo non coerente.");
            var success = await _service.UpdateAsync(id, address);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
