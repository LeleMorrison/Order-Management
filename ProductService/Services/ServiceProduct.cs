using Microsoft.EntityFrameworkCore;
using ProductService.Database;
using Shared.Models;

namespace ProductService.Services
{
    public class ServiceProduct
    {
        private readonly ProductDB _context;
        public ServiceProduct(ProductDB context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateAsync(int id, Product productData)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;
            product.Name = productData.Name;
            product.Price = productData.Price;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
