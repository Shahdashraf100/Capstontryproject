using Capstontryproject.Dtos;
using Capstontryproject.Servses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstontryproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        // الحصول على كل المنتجات
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // الحصول على تفاصيل منتج معين
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }


        [HttpPost("addproduct")]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductDTO productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Invalid product data.");
            }

            var response = await _productService.AddProductAsync(productDto);

            if (response.Success)
            {
                return CreatedAtAction(nameof(GetProductById), new { id = response.Data.Id }, response.Data);
            }

            return BadRequest(response.Message);
        }

    }

}
