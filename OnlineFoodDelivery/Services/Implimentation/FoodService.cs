using Microsoft.EntityFrameworkCore;
using OnlineFoodDelivery.Data;
using OnlineFoodDelivery.Data.Models;
using OnlineFoodDelivery.Dtos.Requests;
using OnlineFoodDelivery.Dtos.Responses;
using OnlineFoodDelivery.Services.Interface;

namespace OnlineFoodDelivery.Services.Implimentation
{
    public class FoodService : IFood
    {
        private readonly AppDbContext _context;
        private readonly ILogger<FoodService> _logger;

        public FoodService(AppDbContext context, ILogger<FoodService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ResponseDto<bool>> AddFoodAsync(CreateFoodDto dto)
        {
            var restaurant = await _context.Restaurants.FindAsync(dto.RestaurantId);
            if (restaurant == null)
                return ResponseDto<bool>.Failure("Restaurant not found.");

            var food = new Food
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                RestaurantId = dto.RestaurantId,
                CreatedAt = dto.CreatedAt,
                CreatedBy = dto.CreatedBy
            };

            _context.Foods.Add(food);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Food {food.Name} added to restaurant {restaurant.Name}.");

            return ResponseDto<bool>.SuccessResponse(true, "Food added successfully.");
        }

        public async Task<ResponseDto<List<Food>>> GetFoodsByRestaurantAsync(int restaurantId)
        {
            var foods = await _context.Foods.Where(f => f.RestaurantId == restaurantId).ToListAsync();
            return ResponseDto<List<Food>>.SuccessResponse(foods, "Foods retrieved successfully.");
        }

        public async Task<ResponseDto<bool>> UpdateFoodAsync(int foodId, UpdateFoodDto dto)
        {
            var food = await _context.Foods.FindAsync(foodId);
            if (food == null)
                return ResponseDto<bool>.Failure("Food not found.");

            food.Name = dto.Name;
            food.Description = dto.Description;
            food.Price = dto.Price;
            food.UpdatedAt = DateTime.UtcNow;
            food.UpdatedBy = dto.UpdatedBy;

            _context.Foods.Update(food);
            await _context.SaveChangesAsync();

            return ResponseDto<bool>.SuccessResponse(true, "Food updated successfully.");
        }

        public async Task<ResponseDto<bool>> DeleteFoodAsync(int foodId)
        {
            var food = await _context.Foods.FindAsync(foodId);
            if (food == null)
                return ResponseDto<bool>.Failure("Food not found.");

            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();
            return ResponseDto<bool>.SuccessResponse(true, "Food deleted successfully.");
        }
    }
}
