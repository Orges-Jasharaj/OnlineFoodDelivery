using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineFoodDelivery.Services.Interface;
using System.Security.Claims;

namespace OnlineFoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurant _restaurant;

        public RestaurantController(IRestaurant restaurant)
        {
            _restaurant = restaurant;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRestaurant([FromBody] Dtos.Requests.CreateRestaurantDto createRestaurantDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }
            createRestaurantDto.CreatedBy = userId;
            createRestaurantDto.CreatedAt = DateTime.UtcNow;
            return Ok(await _restaurant.CreateRestaurantAsync(createRestaurantDto));
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllRestaurants()
        {
            return Ok(await _restaurant.GetAllRestaurantsAsync());
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetRestaurantById(int id)
        {
            return Ok(await _restaurant.GetRestaurantByIdAsync(id));
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] Dtos.Requests.CreateRestaurantDto updateRestaurantDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }
            updateRestaurantDto.UpdatedBy = userId;
            updateRestaurantDto.UpdatedAt = DateTime.UtcNow;
            return Ok(await _restaurant.UpdateRestaurantAsync(id, updateRestaurantDto));
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            return Ok(await _restaurant.DeleteRestaurantAsync(id));
        }

        [HttpGet("getByOwner/{ownerId}")]
        public async Task<IActionResult> GetRestaurantsByOwnerId(string ownerId)
        {
            return Ok(await _restaurant.GetRestaurantsByOwnerIdAsync(ownerId));
        }

        [HttpPost("addFood/{restaurantId}")]
        public async Task<IActionResult> AddFoodToRestaurant(int restaurantId, [FromBody] Dtos.Requests.AddFoodToRestaurantDto addFood)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }
            addFood.CreatedBy = userId;
            addFood.CreatedAt = DateTime.UtcNow;
            return Ok(await _restaurant.AddFoodToRestaurant(restaurantId, addFood));
        }




    }
}
