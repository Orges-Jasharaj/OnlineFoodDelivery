using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineFoodDelivery.Dtos.Requests;
using OnlineFoodDelivery.Services.Interface;
using System.Security.Claims;

namespace OnlineFoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItem _orderItemService;
        public OrderItemController(IOrderItem orderItemService)
        {
            _orderItemService = orderItemService;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddItemToOrder([FromBody] AddOrderItemDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }
            dto.CreatedBy = userId;
            dto.CreatedAt = DateTime.UtcNow;
            return Ok(await _orderItemService.AddItemToOrder(dto));
        }

        [HttpDelete("remove/{itemId}")]
        public async Task<IActionResult> RemoveItemFromOrder(int itemId)
        {
            return Ok(await _orderItemService.RemoveItemFromOrder(itemId));
        }
    }
}
