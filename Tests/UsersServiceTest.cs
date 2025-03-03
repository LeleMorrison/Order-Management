using Microsoft.EntityFrameworkCore;
using UserService.Database;
using System;
using System.Threading.Tasks;
using Xunit;
using UserService.Services;
using Shared.Models;
using AddressService.Database;
using AddressService.Services;
namespace TestCollaudo
{
    public class UsersServiceTest
    {

        [Fact]
        public async Task CreateUser()
        {
            var options = new DbContextOptionsBuilder<UserDB>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;
            using var context = new UserDB(options);
            var service = new ServiceUser(context);

            var user = new User { Name = "Mario Rossi", Email = "mario@rossi.it" };
            var created = await service.CreateAsync(user);
            Assert.True(created.Id > 0);

            var fetched = await service.GetByIdAsync(created.Id);
            Assert.NotNull(fetched);
            Assert.Equal("Mario Rossi", fetched!.Name);
            Assert.Equal("mario@rossi.it", fetched.Email);
        }

        [Fact]
        public async Task UpdateUser() 
        {
            var options = new DbContextOptionsBuilder<UserDB>()
                                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                                .Options;
            using var context = new UserDB(options);
            var service = new ServiceUser(context);

            var address = await service.CreateAsync(new User { Name = "Pippo", Email = "pippo@pluto.it"});
            var updatedData = new User { Name = "Pippo Pluto" };
            bool updated = await service.UpdateAsync(address.Id, updatedData);
            Assert.True(updated);

            var fetched = await service.GetByIdAsync(address.Id);
            Assert.NotNull(fetched);
            Assert.Equal("Pippo Pluto", fetched.Name);
        }

        [Fact]
        public async Task DeleteUser()
        {
            var options = new DbContextOptionsBuilder<UserDB>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;
            using var context = new UserDB(options);
            var service = new ServiceUser(context);

            // Tenta di eliminare un utente inesistente (ID arbitrario)
            bool result = await service.DeleteAsync(999);
            Assert.False(result);
        }
    
}
}
