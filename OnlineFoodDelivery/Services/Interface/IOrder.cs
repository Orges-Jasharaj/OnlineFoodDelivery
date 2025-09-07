using OnlineFoodDelivery.Dtos.Requests;
using OnlineFoodDelivery.Dtos.Responses;

namespace OnlineFoodDelivery.Services.Interface
{
    public interface IOrder
    {
        Task<ResponseDto<bool>> CreateOrderAsync(CreateOrderDto dto);
        Task<ResponseDto<Orders>> GetOrderByIdAsync(int orderId);
        Task<ResponseDto<List<Orders>>> GetOrdersByClientAsync(string clientId);
        Task<ResponseDto<List<Orders>>> GetAllOrdersAsync();
        Task<ResponseDto<bool>> UpdateOrderStatusAsync(int orderId, string status, string updatedBy);
        Task<ResponseDto<bool>> DeleteOrderAsync(int orderId);
    }
}
