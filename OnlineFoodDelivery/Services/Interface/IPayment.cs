using OnlineFoodDelivery.Data.Models;
using OnlineFoodDelivery.Dtos.Requests;
using OnlineFoodDelivery.Dtos.Responses;

namespace OnlineFoodDelivery.Services.Interface
{
    public interface IPayment
    {
        Task<ResponseDto<bool>> MakePaymentAsync(MakePaymentDto dto);
        Task<ResponseDto<bool>> UpdatePaymentStatusAsync(int paymentId, string status, string updatedBy);
        Task<ResponseDto<Payment>> GetPaymentByOrderIdAsync(int orderId);
        Task<List<Payment>> GetAllPaymentsAsync();
    }
}
