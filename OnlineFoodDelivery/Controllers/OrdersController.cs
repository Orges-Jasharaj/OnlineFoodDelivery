using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineFoodDelivery.Services.Interface;
using System.Security.Claims;

namespace OnlineFoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrder _order;

        public OrdersController(IOrder order)
        {
            _order = order;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _order.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _order.GetOrderByIdAsync(orderId);
            if (order.Data == null)
                return NotFound(order);
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Dtos.Requests.CreateOrderDto createOrderDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }
            createOrderDto.CreatedBy = userId;
            createOrderDto.CreatedAt = DateTime.UtcNow;
            return Ok(await _order.CreateOrderAsync(createOrderDto));
        }

        [HttpPut("{orderId}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromQuery] string status)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }
            return Ok(await _order.UpdateOrderStatusAsync(orderId, status, userId));
        }


        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            return Ok(await _order.DeleteOrderAsync(orderId));
        }

        [HttpPost]
        [Route("client/{clientId}")]
        public async Task<IActionResult> GetOrdersByClient(string clientId)
        {
            var orders = await _order.GetOrdersByClientAsync(clientId);
            return Ok(orders);
        }

    }
}
