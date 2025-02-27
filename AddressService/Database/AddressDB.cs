using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace AddressService.Database
{
    public class AddressDB : DbContext
    {
        public AddressDB(DbContextOptions<AddressDB> options) : base(options) { }
        public DbSet<Address> Addresses { get; set; } = null!;
    }
}
