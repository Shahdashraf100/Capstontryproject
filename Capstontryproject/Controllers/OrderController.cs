using Capstontryproject.Dtos;
using Capstontryproject.Servses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstontryproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // تقديم طلب جديد
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO createOrderDto)
        {
            var result = await _orderService.CreateOrderAsync(createOrderDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // الحصول على كل الطلبات الخاصة بالمستخدم
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetOrdersAsync();
            return Ok(orders);
        }
    }

}
