using OnlineFoodDelivery.Dtos.Requests;
using OnlineFoodDelivery.Dtos.Responses;

namespace OnlineFoodDelivery.Services.Interface
{
    public interface IRestaurant
    {
        Task<ResponseDto<bool>> CreateRestaurantAsync(CreateRestaurantDto createRestaurantDto);
        Task<ResponseDto<List<RestaurantDto>>> GetAllRestaurantsAsync();
        Task<ResponseDto<RestaurantDto>> GetRestaurantByIdAsync(int id);
        Task<ResponseDto<bool>> UpdateRestaurantAsync(int id, CreateRestaurantDto updateRestaurantDto);
        Task<ResponseDto<bool>> DeleteRestaurantAsync(int id);
        Task<ResponseDto<List<RestaurantDto>>> GetRestaurantsByOwnerIdAsync(string ownerId);
        Task<ResponseDto<bool>> AddFoodToRestaurant(int restaurantId, AddFoodToRestaurantDto addFood);


    }
}
