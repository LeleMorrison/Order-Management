using Microsoft.EntityFrameworkCore;
using OrdersService.Database;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Services
{
    public class ServiceOrders
    {
        private readonly OrderDB _context;
        public ServiceOrders(OrderDB context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            // Eager loading degli OrderItem
            return await _context.Orders
                .Include(o => o.Items)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            // Salviamo l'ordine e i suoi OrderItem
            // EF creerà automaticamente record in OrderItems
            // perché Items è collegato.
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var existing = await _context.Orders.FindAsync(id);
            if (existing == null) return false;

            _context.Orders.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, Order orderData)
        {
            var product = await _context.Orders.FindAsync(id);
            if (product == null) return false;
            product.OrderDate = orderData.OrderDate;
            product.AddressId= orderData.AddressId;
            product.UserId = orderData.UserId;
            product.Items = orderData.Items;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
