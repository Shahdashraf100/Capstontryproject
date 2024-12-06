using Capstontryproject.Dtos;
using Capstontryproject.Servses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstontryproject.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        // الحصول على العربة للمستخدم
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var cart = await _cartService.GetCartAsync();
            return Ok(cart);
        }

        // إضافة منتج إلى العربة
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDTO addToCartDto)
        {
            var result = await _cartService.AddToCartAsync(addToCartDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // حذف منتج من العربة
        [HttpDelete("remove/{productId}")]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var result = await _cartService.RemoveFromCartAsync(productId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }

}
