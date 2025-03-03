using AddressService.Database;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace AddressService.Services
{
    public class ServiceAddress
    {
        private readonly AddressDB _context;
        public ServiceAddress(AddressDB context)
        {
            _context = context;
        }
        // Ottengo tutti gli indirizzi
        public async Task<List<Address>> GetAllAsync()
        {
            return await _context.Addresses.ToListAsync();
        }
        // Ottengo un indirizzo per ID
        public async Task<Address?> GetByIdAsync(int id)
        {
            return await _context.Addresses.FindAsync(id);
        }
        // Creo un nuovo indirizzo
        public async Task<Address> CreateAsync(Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return address;
        }
        // Aggiorno un indirizzo
        public async Task<bool> UpdateAsync(int id, Address addressData)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null) return false;
            address.UserId = addressData.UserId;
            address.Street = addressData.Street;
            address.City = addressData.City;
            await _context.SaveChangesAsync();
            return true;
        }
        // Elimino un indirizzo
        public async Task<bool> DeleteAsync(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null) return false;
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
