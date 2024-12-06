using Capstontryproject.Dtos;
using Capstontryproject.Models;
using Microsoft.EntityFrameworkCore;

namespace Capstontryproject.Servses
{
    public class CartService
    {
        private readonly dbcontext _context;
        private readonly UserService _userService;

        public CartService(dbcontext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // الحصول على العربة للمستخدم
        public async Task<ServiceResponse<CartDTO>> GetCartAsync()
        {
            var response = new ServiceResponse<CartDTO>();

            var user = await _userService.GetCurrentUserAsync();

            var cart = await _context.Carts
                .Where(c => c.UserId == user.Id)
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync();

            if (cart == null)
            {
                response.Success = false;
                response.Message = "Cart not found.";
                return response;
            }

            response.Data = new CartDTO
            {
                Id = cart.Id,
                CartItems = cart.CartItems.Select(ci => new CartItemDTO
                {
                    ProductId = ci.ProductId,
                    ProductName = ci.Product.Name,
                    Quantity = ci.Quantity,
                    Price = ci.Product.Price
                }).ToList(),
                TotalPrice = cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price)
            };

            return response;
        }

        // إضافة منتج إلى العربة
        public async Task<ServiceResponse<bool>> AddToCartAsync(AddToCartDTO addToCartDto)
        {
            var response = new ServiceResponse<bool>();

            var user = await _userService.GetCurrentUserAsync();
            var product = await _context.products.FindAsync(addToCartDto.ProductId);

            if (product == null)
            {
                response.Success = false;
                response.Message = "Product not found.";
                return response;
            }

            var cart = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (cart == null)
            {
                cart = new Cart { UserId = user.Id, CartItems = new List<CartItem>() };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var cartItem = await _context.cartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cart.Id && ci.ProductId == addToCartDto.ProductId);

            if (cartItem != null)
            {
                cartItem.Quantity += addToCartDto.Quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = addToCartDto.ProductId,
                    Quantity = addToCartDto.Quantity,
                    CartId = cart.Id
                });
            }

            await _context.SaveChangesAsync();

            response.Data = true;
            return response;
        }

        // حذف منتج من العربة
        public async Task<ServiceResponse<bool>> RemoveFromCartAsync(int productId)
        {
            var response = new ServiceResponse<bool>();

            var user = await _userService.GetCurrentUserAsync();
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (cart == null)
            {
                response.Success = false;
                response.Message = "Cart not found.";
                return response;
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (cartItem != null)
            {
                cart.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }

            response.Data = true;
            return response;
        }
    }

}
