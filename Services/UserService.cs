using EjercicioProductos.Context;
using EjercicioProductos.Models;
using Microsoft.EntityFrameworkCore;

namespace EjercicioProductos.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User?> ValidateUserAsync(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
    }
}
