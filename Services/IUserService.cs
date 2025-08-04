using EjercicioProductos.Models;

namespace EjercicioProductos.Services
{
    public interface IUserService
    {
        Task<User?> ValidateUserAsync(string username, string password);

    }
}
