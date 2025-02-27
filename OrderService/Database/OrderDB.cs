using Microsoft.EntityFrameworkCore;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Database
{
    public class OrderDB : DbContext
    {
        public OrderDB(DbContextOptions<OrderDB> options) : base(options) { }

        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> Items { get; set; } = null!;// Tabella Ordini

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurazioni varie
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne()
                .HasForeignKey(i => i.OrderId);
        }
    }
}
