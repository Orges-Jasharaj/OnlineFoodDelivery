using Microsoft.EntityFrameworkCore;
using OnlineFoodDelivery.Data;
using OnlineFoodDelivery.Data.Models;
using OnlineFoodDelivery.Dtos.Requests;
using OnlineFoodDelivery.Dtos.Responses;
using OnlineFoodDelivery.Services.Interface;

namespace OnlineFoodDelivery.Services.Implimentation
{
    public class ReviewService : IReview
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ReviewService> _logger;

        public ReviewService(AppDbContext context, ILogger<ReviewService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ResponseDto<bool>> AddReviewAsync(CreateReviewDto dto)
        {
            var restaurant = await _context.Restaurants.FindAsync(dto.RestaurantId);
            if (restaurant == null)
                return ResponseDto<bool>.Failure("Restaurant not found.");

            var review = new Review
            {
                RestaurantId = dto.RestaurantId,
                ClientId = dto.ClientId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedAt = dto.CreatedAt,
                CreatedBy = dto.CreatedBy
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Review added for restaurant {restaurant.Name} by user {dto.ClientId}.");

            return ResponseDto<bool>.SuccessResponse(true, "Review added successfully.");
        }

        public async Task<ResponseDto<List<Review>>> GetReviewsByRestaurantAsync(int restaurantId)
        {
            var reviews = await _context.Reviews.Where(r => r.RestaurantId == restaurantId).ToListAsync();
            return ResponseDto<List<Review>>.SuccessResponse(reviews, "Reviews retrieved successfully.");
        }

        public async Task<ResponseDto<bool>> DeleteReviewAsync(int reviewId, string userId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review == null)
                return ResponseDto<bool>.Failure("Review not found.");

            if (review.ClientId != userId)
                return ResponseDto<bool>.Failure("You cannot delete someone else's review.");

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return ResponseDto<bool>.SuccessResponse(true, "Review deleted successfully.");
        }

        public async Task<ResponseDto<List<Review>>> GetAllReviewsAsync()
        {
            var reviews = await _context.Reviews.ToListAsync();
            return ResponseDto<List<Review>>.SuccessResponse(reviews, "All reviews retrieved successfully.");
        }
    }
}
