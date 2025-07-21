using EjercicioProductos.Models;

namespace EjercicioProductos.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProduct();
        Task<Product> GetProductId(short C_product);
        Task InsertProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(short C_productId);
    }
}
