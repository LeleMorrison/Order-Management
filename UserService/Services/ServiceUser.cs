using Microsoft.EntityFrameworkCore;
using Shared.Models;
using UserService.Database;

namespace UserService.Services
{
    public class ServiceUser
    {
        private readonly UserDB _context;
        public ServiceUser(UserDB context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateAsync(int id, User userData)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            user.Name = userData.Name;
            user.Email = userData.Email;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
