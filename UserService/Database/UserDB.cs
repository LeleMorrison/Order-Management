using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace UserService.Database
{
    public class UserDB : DbContext
    {
        public UserDB(DbContextOptions<UserDB> options) : base(options) { }
        public DbSet<User> Users { get; set; } = null!;
    }
}
