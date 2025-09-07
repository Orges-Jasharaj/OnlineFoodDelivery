using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineFoodDelivery.Dtos.Requests;
using OnlineFoodDelivery.Services.Interface;
using System.Security.Claims;

namespace OnlineFoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFood _foodService;


        public FoodController(IFood foodService)
        {
            _foodService = foodService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddFood([FromBody] CreateFoodDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }
            dto.CreatedBy = userId;
            dto.CreatedAt = DateTime.UtcNow;
            return Ok(await _foodService.AddFoodAsync(dto));
        }

        [HttpGet("restaurant/{restaurantId}")]
        public async Task<IActionResult> GetFoodsByRestaurant(int restaurantId)
        {
            return Ok(await _foodService.GetFoodsByRestaurantAsync(restaurantId));
        }

        [HttpPut("update/{foodId}")]
        public async Task<IActionResult> UpdateFood(int foodId, [FromBody] UpdateFoodDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }
            dto.UpdatedBy = userId;
            return Ok(await _foodService.UpdateFoodAsync(foodId, dto));
        }
    }
}
