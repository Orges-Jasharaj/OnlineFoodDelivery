using Microsoft.EntityFrameworkCore;
using OnlineFoodDelivery.Data;
using OnlineFoodDelivery.Data.Models;
using OnlineFoodDelivery.Dtos.Requests;
using OnlineFoodDelivery.Dtos.Responses;
using OnlineFoodDelivery.Services.Interface;
using Orders = OnlineFoodDelivery.Data.Models.Orders;



namespace OnlineFoodDelivery.Services.Implementation
{
    public class OrderService : IOrder
    {
        private readonly AppDbContext _context;
        private readonly ILogger<OrderService> _logger;

        public OrderService(AppDbContext context, ILogger<OrderService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ResponseDto<bool>> CreateOrderAsync(CreateOrderDto dto)
        {
            var order = new Orders
            {
                ClientId = dto.ClientId,
                DriverId = dto.DriverId,
                Status = "Pending",
                TotalPrice = 0, 
                CreatedAt = dto.CreatedAt,
                CreatedBy = dto.CreatedBy
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Order {order.Id} created successfully.");
            return ResponseDto<bool>.SuccessResponse(true, "Order created successfully.");
        }

        

        public async Task<ResponseDto<List<Orders>>> GetOrdersByClientAsync(string clientId)
        {
            var orders = await _context.Orders
                .Where(o => o.ClientId == clientId)
                .Include(o => o.OrderItems)
                .ToListAsync();

            return ResponseDto<List<Orders>>.SuccessResponse(orders, "Orders retrieved successfully.");
        }

        public async Task<ResponseDto<bool>> UpdateOrderStatusAsync(int orderId, string status, string updatedBy)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
                return ResponseDto<bool>.Failure("Order not found.");

            order.Status = status;
            order.UpdatedAt = DateTime.UtcNow;
            order.UpdatedBy = updatedBy;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Order {order.Id} status updated to {status}.");

            return ResponseDto<bool>.SuccessResponse(true, "Order status updated successfully.");
        }

        public async Task<ResponseDto<bool>> DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
                return ResponseDto<bool>.Failure("Order not found.");

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Order {order.Id} deleted.");

            return ResponseDto<bool>.SuccessResponse(true, "Order deleted successfully.");
        }

        public async Task<ResponseDto<Dtos.Responses.Orders>> GetOrderByIdAsync(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Food)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {

                return ResponseDto<Dtos.Responses.Orders>.Failure("Order not found.");
            }
            var orderDto = new Dtos.Responses.Orders
            {
                Id = order.Id,
                ClientId = order.ClientId,
                DriverId = order.DriverId,
                Status = order.Status,
                TotalPrice = order.TotalPrice,
                CreatedBy = order.CreatedBy,
                CreatedAt = order.CreatedAt,
                UpdatedBy = order.UpdatedBy,
                UpdatedAt = order.UpdatedAt,
                OrderItems = order.OrderItems
            };
            return ResponseDto<Dtos.Responses.Orders>.SuccessResponse(orderDto, "Order retrieved successfully.");
        }

        public async Task<ResponseDto<List<Dtos.Responses.Orders>>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Food)
                .ToListAsync();
            var orderDtos = orders.Select(order => new Dtos.Responses.Orders
            {
                Id = order.Id,
                ClientId = order.ClientId,
                DriverId = order.DriverId,
                Status = order.Status,
                TotalPrice = order.TotalPrice,
                CreatedBy = order.CreatedBy,
                CreatedAt = order.CreatedAt,
                UpdatedBy = order.UpdatedBy,
                UpdatedAt = order.UpdatedAt,
                OrderItems = order.OrderItems
            }).ToList();
            return ResponseDto<List<Dtos.Responses.Orders>>.SuccessResponse(orderDtos, "Orders retrieved successfully.");
        }
    }
}
