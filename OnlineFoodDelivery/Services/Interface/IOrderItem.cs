using OnlineFoodDelivery.Dtos.Requests;
using OnlineFoodDelivery.Dtos.Responses;

namespace OnlineFoodDelivery.Services.Interface
{
    public interface IOrderItem
    {
        Task<ResponseDto<bool>> AddItemToOrder(AddOrderItemDto dto);
        Task<ResponseDto<bool>> RemoveItemFromOrder(int itemId);
    }
}
