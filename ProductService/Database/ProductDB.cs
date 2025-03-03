using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace ProductService.Database
{
    public class ProductDB : DbContext
    {
        public ProductDB(DbContextOptions<ProductDB> options) : base(options) { }
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Category { get; set; } = null!;
    }
}
