using AddressService.Database;
using Microsoft.EntityFrameworkCore;
using OrdersService.Database;
using OrdersService.Services;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCollaudo
{
    public class OrdersServiceTest
    {
        OrderItem FirstItem = new OrderItem()
        {
            OrderId = 5,
            ProductId = 4,
            Quantity = 5,
        };

        [Fact]
        public async Task CreateOrder()
        {
            var options = new DbContextOptionsBuilder<OrderDB>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;
            using var context = new OrderDB(options);
            var service = new ServiceOrders(context);


            // Crea un ordine di prova
            var order = new Order { UserId = 1, OrderDate = DateTime.Now , AddressId = 1, Items = [this.FirstItem] };
            var created = await service.CreateOrderAsync(order);
            Assert.True(created.Id > 0);
            // OrderDate dovrebbe essere valorizzata
            Assert.NotEqual(default, created.OrderDate);

            // Recupera l'ordine e verifica i campi
            var fetched = await service.GetOrderByIdAsync(created.Id);
            Assert.NotNull(fetched);
            Assert.Equal(1, fetched!.AddressId);
            Assert.Equal(1, fetched.UserId);

        }

        [Fact]
        public async Task DeleteOrder()
        {
            var options = new DbContextOptionsBuilder<OrderDB>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;
            using var context = new OrderDB(options);
            var service = new ServiceOrders(context);

            // Crea ed elimina un ordine
            var order = await service.CreateOrderAsync(new Order { UserId = 1, OrderDate = DateTime.Now, AddressId = 1, Items = [this.FirstItem] } );
            bool deleted = await service.DeleteOrderAsync(order.Id);
            Assert.True(deleted);
            var fetched = await service.GetOrderByIdAsync(order.Id);
            Assert.Null(fetched);
        }
    }
}
