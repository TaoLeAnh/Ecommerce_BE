using Microsoft.AspNetCore.Mvc;
using EcommerceBackend.Services;
using EcommerceBackend.Data;
using Microsoft.EntityFrameworkCore;
using EcommerceBackend.Models;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ApplicationDbContext _context;

        public ProductController(IProductService productService, ApplicationDbContext context)
        {
            _productService = productService;
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            var createdProduct = await _productService.CreateProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.ProductId }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }
            await _productService.UpdateProduct(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProduct(id);
            return NoContent();
        }
        [HttpPost("search")]
        public async Task<ActionResult<PagedResult<Product>>> SearchProducts([FromBody] ProductFilterDto filter)
        {
            var query = await _productService.FilterProducts(filter);
            return Ok(query);
        }
        [HttpGet("{id}/also-viewed")]
        public async Task<IActionResult> GetCustomerAlsoViewed(int id)
        {
            var currentProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (currentProduct == null)
                return NotFound("Sản phẩm không tồn tại");

            var relatedProducts = await _context.Products
                .Where(p => p.ProductId != id && p.CategoryId == currentProduct.CategoryId)
                .Take(4)
                .ToListAsync();
            return Ok(relatedProducts);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
            return Ok(products);
        }

    }
}