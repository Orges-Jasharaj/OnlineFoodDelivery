using Microsoft.EntityFrameworkCore;
using OnlineFoodDelivery.Data;
using OnlineFoodDelivery.Data.Models;
using OnlineFoodDelivery.Dtos.Requests;
using OnlineFoodDelivery.Dtos.Responses;
using OnlineFoodDelivery.Services.Interface;

namespace OnlineFoodDelivery.Services.Implimentation
{
    public class PaymentService : IPayment
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(AppDbContext context, ILogger<PaymentService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ResponseDto<bool>> MakePaymentAsync(MakePaymentDto dto)
        {
            var order = await _context.Orders.FindAsync(dto.OrderId);
            if (order == null)
                return ResponseDto<bool>.Failure("Order not found.");

            if (dto.Amount < order.TotalPrice)
                return ResponseDto<bool>.Failure("Payment amount is less than order total.");

            var payment = new Payment
            {
                OrderId = dto.OrderId,
                Amount = dto.Amount,
                PaymentMethod = dto.PaymentMethod,
                PaymentStatus = "Pending", 
                CreatedAt = dto.CreatedAt,
                CreatedBy = dto.CreatedBy
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Payment {payment.Id} created for order {order.Id} with status Pending.");

            return ResponseDto<bool>.SuccessResponse(true, "Payment initialized successfully (Pending).");
        }

        public async Task<ResponseDto<bool>> UpdatePaymentStatusAsync(int paymentId, string status, string updatedBy)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment == null)
                return ResponseDto<bool>.Failure("Payment not found.");

            payment.PaymentStatus = status;
            payment.UpdatedAt = DateTime.UtcNow;
            payment.UpdatedBy = updatedBy;

            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Payment {payment.Id} status updated to {status}.");

            return ResponseDto<bool>.SuccessResponse(true, $"Payment status updated to {status}.");
        }

        public async Task<ResponseDto<Payment>> GetPaymentByOrderIdAsync(int orderId)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.OrderId == orderId);
            if (payment == null)
                return ResponseDto<Payment>.Failure("Payment not found.");

            return ResponseDto<Payment>.SuccessResponse(payment, "Payment retrieved successfully.");
        }

        public async Task<List<Payment>> GetAllPaymentsAsync()
        {
            return await _context.Payments.ToListAsync();
        }
    }
}
