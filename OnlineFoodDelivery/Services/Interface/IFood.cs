using OnlineFoodDelivery.Data.Models;
using OnlineFoodDelivery.Dtos.Requests;
using OnlineFoodDelivery.Dtos.Responses;

namespace OnlineFoodDelivery.Services.Interface
{
    public interface IFood
    {
        Task<ResponseDto<bool>> AddFoodAsync(CreateFoodDto dto);
        Task<ResponseDto<List<Food>>> GetFoodsByRestaurantAsync(int restaurantId);
        Task<ResponseDto<bool>> UpdateFoodAsync(int foodId, UpdateFoodDto dto);
    }
}
