using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineFoodDelivery.Dtos.Requests;
using OnlineFoodDelivery.Services.Interface;
using System.Security.Claims;

namespace OnlineFoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReview _reviewService;

        public ReviewController(IReview reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReviewsByRestaurant(int restaurantId)
        {
            return Ok(await _reviewService.GetReviewsByRestaurantAsync(restaurantId));
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] CreateReviewDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }
            dto.ClientId = userId;
            dto.CreatedAt = DateTime.UtcNow;
            dto.CreatedBy = userId;
            return Ok(await _reviewService.AddReviewAsync(dto));
        }

        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }
            return Ok(await _reviewService.DeleteReviewAsync(reviewId, userId));
        }

        [HttpGet("getallreviews")]
        public async Task<IActionResult> GetAllReviews()
        {
            return Ok(await _reviewService.GetAllReviewsAsync());
        }

    }
}
