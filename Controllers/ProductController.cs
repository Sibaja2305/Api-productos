using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EjercicioProductos.Models;
using EjercicioProductos.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EjercicioProductos.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [Authorize(Roles = "guest,user")]
        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            List<Product> products = await _productService.GetProduct();
            List<Product> productsOrder = products.OrderBy(p => p.D_name).ToList();
            return Ok(productsOrder);
        }
        [Authorize(Roles = "guest,user")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(short id)
        {
            Product? product = await _productService.GetProductId(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            await _productService.InsertProduct(product);
            return Ok();
        }
        [Authorize(Roles = "user")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(short id, [FromBody] Product product)
        {
            product.C_product = id;
            await _productService.UpdateProduct(product);
            return Ok();
        }
        [Authorize(Roles = "user")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(short id)
        {
            await _productService.DeleteProduct(id);
            return Ok();
        }
        [Authorize(Roles = "guest,user")]
        [HttpGet("paged")]
        public async Task<ActionResult<List<Product>>> GetPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
                return BadRequest("Valores de paginación inválidos.");

            var products = await _productService.GetProductsPaged(pageNumber, pageSize);
            return Ok(products);
        }
    }

}
