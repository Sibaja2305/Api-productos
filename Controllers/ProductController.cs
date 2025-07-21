using System.Threading.Tasks;
using EjercicioProductos.Models;
using EjercicioProductos.Services;
using Microsoft.AspNetCore.Mvc;

namespace EjercicioProductos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController: ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            var products = await _productService.GetProduct();
            var productsOrder = products.OrderBy(p => p.D_name).ToList();
            return Ok(productsOrder);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(short id) {
            var product = await _productService.GetProductId(id);
            if (product == null) 
                return NotFound();
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            await _productService.InsertProduct(product);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(short id, [FromBody] Product product)
        {
            product.C_product = id;
            await _productService.UpdateProduct(product);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(short id)
        {
            await _productService.DeleteProduct(id);
            return Ok();
        }
    }
}
