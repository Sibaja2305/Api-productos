using EjercicioProductos.Context;
using EjercicioProductos.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            List<Product> products = await _context.Products
                .FromSqlRaw("EXEC sp_GetProduct")
                .AsNoTracking()
                .ToListAsync();

            return products;
        }

        public async Task<Product?> GetProductId(short C_product)
        {
            List<Product> productos = await _context.Products
                .FromSqlInterpolated($"EXEC sp_GetProductID @Id = {C_product}")
                .AsNoTracking()
                .ToListAsync();

            Product? producto = productos.FirstOrDefault();
            return producto;
        }

        public async Task<List<Product>> GetProductsPaged(int pageNumber, int pageSize)
        {
            return await _context.Products
                .FromSqlInterpolated($"EXEC sp_GetProductPaged @PageNumber = {pageNumber}, @PageSize = {pageSize}")
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task InsertProduct(Product product)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_InsertProduct @Name = {0}, @Price = {1}, @stock = {2}",
                product.D_name, product.P_price, product.Q_stock
            );
        }

        public async Task UpdateProduct(Product product)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_UpdateProduct @Id = {0},@Name = {1}, @Price = {2}, @stock = {3}",
                product.C_product, product.D_name, product.P_price, product.Q_stock
            );
        }
    }
}
