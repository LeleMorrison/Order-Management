using Microsoft.EntityFrameworkCore;
using ProductService.Database;
using ProductService.Services;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Database;
using UserService.Services;

namespace TestCollaudo
{
    public class ProductServiceTest
    {
        [Fact]
        public async Task CreateProduct()
        {
            // Configura un contesto EF Core in memoria per i test
            var options = new DbContextOptionsBuilder<ProductDB>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;
            using var context = new ProductDB(options);
            var service = new ServiceProduct(context);

            // Crea un nuovo prodotto
            var newProduct = new Product { Name = "TestProduct", Price = 9.99m };
            var created = await service.CreateAsync(newProduct);

            // Verifica che l'ID sia assegnato e che il prodotto sia recuperabile
            Assert.True(created.Id > 0);
            var fetched = await service.GetByIdAsync(created.Id);
            Assert.NotNull(fetched);
            Assert.Equal("TestProduct", fetched!.Name);
            Assert.Equal(9.99m, fetched.Price);
        }

        [Fact]
        public async Task UpdateProduct()
        {
            var options = new DbContextOptionsBuilder<ProductDB>()
                                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                                .Options;
            using var context = new ProductDB(options);
            var service = new ServiceProduct(context);

            var address = await service.CreateAsync(new Product { Name = "Pennello", Category = "Arte", Price = 20});
            var updatedData = new Product { Category = "Bricolage", Price = 15.55m };
            bool updated = await service.UpdateAsync(address.Id, updatedData);
            Assert.True(updated);

            var fetched = await service.GetByIdAsync(address.Id);
            Assert.NotNull(fetched);
            Assert.Equal("Bricolage", fetched.Category);
            Assert.Equal(15.55m, fetched.Price);
        }

        [Fact]
        public async Task DeleteProduct()
        {
            var options = new DbContextOptionsBuilder<ProductDB>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;
            using var context = new ProductDB(options);
            var service = new ServiceProduct(context);

            // Inserisce un prodotto di test
            var product = await service.CreateAsync(new Product { Name = "ToDelete", Price = 1.0m });
            int id = product.Id;
            // Elimina il prodotto
            var result = await service.DeleteAsync(id);
            // Verifica eliminazione avvenuta
            Assert.True(result);
            var deletedFetch = await service.GetByIdAsync(id);
            Assert.Null(deletedFetch);
        }
    }
}
