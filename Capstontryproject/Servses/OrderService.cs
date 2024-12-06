using Capstontryproject.Dtos;
using Capstontryproject.Models;
using Microsoft.EntityFrameworkCore;

namespace Capstontryproject.Servses
{
    public class OrderService
    {
        private readonly dbcontext _context;
        private readonly CartService _cartService;
        private readonly UserService _userService;

        public OrderService(dbcontext context, CartService cartService, UserService userService)
        {
            _context = context;
            _cartService = cartService;
            _userService = userService;
        }

        // تقديم طلب جديد
        public async Task<ServiceResponse<OrderDTO>> CreateOrderAsync(CreateOrderDTO createOrderDto)
        {
            var response = new ServiceResponse<OrderDTO>();

            var user = await _userService.GetCurrentUserAsync();
            var cart = await _cartService.GetCartAsync();

            if (cart.Data.CartItems.Count == 0)
            {
                response.Success = false;
                response.Message = "Cart is empty.";
                return response;
            }

            var order = new Order
            {
                UserId = user.Id,
                OrderDate = DateTime.Now,
                TotalAmount = cart.Data.TotalPrice,
                OrderItems = cart.Data.CartItems.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    Price = ci.Price
                }).ToList()
            };

            _context.orders.Add(order);
            await _context.SaveChangesAsync();

            response.Data = new OrderDTO
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDTO
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            };

            return response;
        }

        // الحصول على كل الطلبات للمستخدم
        public async Task<List<OrderDTO>> GetOrdersAsync()
        {
            var user = await _userService.GetCurrentUserAsync();

            return await _context.orders
                .Where(o => o.UserId == user.Id)
                .Select(o => new OrderDTO
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    OrderItems = o.OrderItems.Select(oi => new OrderItemDTO
                    {
                        ProductId = oi.ProductId,
                        ProductName = oi.Product.Name,
                        Quantity = oi.Quantity,
                        Price = oi.Price
                    }).ToList()
                })
                .ToListAsync();
        }
    }

}
