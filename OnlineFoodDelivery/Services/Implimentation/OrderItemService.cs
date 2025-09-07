using Microsoft.EntityFrameworkCore;
using OnlineFoodDelivery.Data;
using OnlineFoodDelivery.Data.Models;
using OnlineFoodDelivery.Dtos.Requests;
using OnlineFoodDelivery.Dtos.Responses;
using OnlineFoodDelivery.Services.Interface;

namespace OnlineFoodDelivery.Services.Implimentation
{
    public class OrderItemService : IOrderItem
    {
        private readonly AppDbContext _context;
        private readonly ILogger<OrderItemService> _logger;

        public OrderItemService(AppDbContext context, ILogger<OrderItemService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ResponseDto<bool>> AddItemToOrder(AddOrderItemDto dto)
        {
            var order = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == dto.OrderId);
            if (order == null)
                return ResponseDto<bool>.Failure("Order not found.");

            var food = await _context.Foods.FindAsync(dto.FoodId);
            if (food == null)
                return ResponseDto<bool>.Failure("Food not found.");

            var orderItem = new OrderItem
            {
                OrderId = dto.OrderId,
                FoodId = dto.FoodId,
                Quantity = dto.Quantity,
                Price = food.Price * dto.Quantity,
                CreatedAt = dto.CreatedAt,
                CreatedBy = dto.CreatedBy
            };

            order.TotalPrice += orderItem.Price;

            _context.OrderItems.Add(orderItem);
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Food {food.Name} added to order {order.Id}.");
            return ResponseDto<bool>.SuccessResponse(true, "Item added to order successfully.");
        }

        public async Task<ResponseDto<bool>> RemoveItemFromOrder(int itemId)
        {
            var item = await _context.OrderItems.FindAsync(itemId);
            if (item == null)
                return ResponseDto<bool>.Failure("Item not found.");

            var order = await _context.Orders.FindAsync(item.OrderId);
            if (order != null)
                order.TotalPrice -= item.Price;

            _context.OrderItems.Remove(item);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Order item {item.Id} removed.");
            return ResponseDto<bool>.SuccessResponse(true, "Item removed successfully.");
        }
    }
}

