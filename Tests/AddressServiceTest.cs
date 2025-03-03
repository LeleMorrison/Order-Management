using AddressService.Database;
using AddressService.Services;
using Microsoft.EntityFrameworkCore;
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
   public class AddressServiceTest
    {
        [Fact]
        public async Task CreateAddress()
        {
            var options = new DbContextOptionsBuilder<AddressDB>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;
            using var context = new AddressDB(options);
            var service = new ServiceAddress(context);

            // Crea un nuovo indirizzo
            var address = new Address { UserId = 1, Street = "Via Foglia 6", City = "Rimini" };
            var created = await service.CreateAsync(address);
            Assert.True(created.Id > 0);

            // Recupera e verifica
            var fetched = await service.GetByIdAsync(created.Id);
            Assert.NotNull(fetched);
            Assert.Equal("Rimini", fetched!.City);
            Assert.Equal(1, fetched.UserId);
            Assert.Equal("Via Foglia 6", fetched.Street);
        }

        [Fact]
        public async Task UpdateAddress()
        {
            var options = new DbContextOptionsBuilder<AddressDB>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;
            using var context = new AddressDB(options);
            var service = new ServiceAddress(context);

            var address = await service.CreateAsync(new Address { UserId = 2, Street = "Via Foglia 6", City = "Rimini" });
            var updatedData = new Address { UserId = 3, Street = "Via Foglia", City = "Riccione" };
            bool updated = await service.UpdateAsync(address.Id, updatedData);
            Assert.True(updated);

            var fetched = await service.GetByIdAsync(address.Id);
            Assert.NotNull(fetched);
            Assert.Equal("Via Foglia", fetched!.Street);
            Assert.Equal(3, fetched.UserId);
            Assert.Equal("Riccione", fetched.City);
        }

        [Fact]

        public async Task DeleteAddress() {
            var options = new DbContextOptionsBuilder<AddressDB>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;
            using var context = new AddressDB(options);
            var service = new ServiceAddress(context);

            // Inserisce un prodotto di test
            var address = await service.CreateAsync(new Address { City = "Rimini", Street = "Via Foglia 6", UserId = 1 });
            int id = address.Id;
            // Elimina l'indirizzo
            var result = await service.DeleteAsync(id);
            // Verifica eliminazione avvenuta
            Assert.True(result);
            var deletedFetch = await service.GetByIdAsync(id);
            Assert.Null(deletedFetch);
        }

    }
}
