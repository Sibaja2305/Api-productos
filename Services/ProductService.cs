using EjercicioProductos.Context;
using EjercicioProductos.Models;
using Microsoft.EntityFrameworkCore;

namespace EjercicioProductos.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }
        public async Task DeleteProduct(short C_productId)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC sp_DeleteProduct @Id = {C_productId}"
            );
        }

        public async Task<List<Product>> GetProduct()
        {
            return await _context.Products
                .FromSqlRaw("EXEC sp_GetProduct")
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product?> GetProductId(short C_product)
        {
            var productos = await _context.Products
                .FromSqlInterpolated($"EXEC sp_GetProductID @Id = {C_product}")
                .AsNoTracking()
                .ToListAsync(); 

            return productos.FirstOrDefault();
        }




        public async Task InsertProduct(Product product)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_InsertProduct @Name = {0}, @Price = {1}, @stock = {2}", product.D_name, product.P_price, product.Q_stock);
        }

        public async Task UpdateProduct(Product product)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_UpdateProduct @Id = {0},@Name = {1}, @Price = {2}, @stock = {3}", product.C_product, product.D_name, product.P_price, product.Q_stock);
        }
    }
}
