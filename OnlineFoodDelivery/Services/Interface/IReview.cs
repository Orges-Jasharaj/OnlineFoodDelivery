using OnlineFoodDelivery.Data.Models;
using OnlineFoodDelivery.Dtos.Requests;
using OnlineFoodDelivery.Dtos.Responses;

namespace OnlineFoodDelivery.Services.Interface
{
    public interface IReview
    {
        Task<ResponseDto<bool>> AddReviewAsync(CreateReviewDto dto);
        Task<ResponseDto<List<Review>>> GetReviewsByRestaurantAsync(int restaurantId);
        Task<ResponseDto<bool>> DeleteReviewAsync(int reviewId, string userId);
        Task<ResponseDto<List<Review>>> GetAllReviewsAsync();
    }
}
