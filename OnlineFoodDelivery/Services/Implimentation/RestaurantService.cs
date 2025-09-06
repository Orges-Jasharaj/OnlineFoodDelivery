using Microsoft.EntityFrameworkCore;
using OnlineFoodDelivery.Data;
using OnlineFoodDelivery.Data.Models;
using OnlineFoodDelivery.Dtos.Requests;
using OnlineFoodDelivery.Dtos.Responses;
using OnlineFoodDelivery.Services.Interface;

namespace OnlineFoodDelivery.Services.Implimentation
{
    public class RestaurantService : IRestaurant
    {
        public readonly AppDbContext _context;
        public ILogger<RestaurantService> _logger;

        public RestaurantService(AppDbContext context, ILogger<RestaurantService> logger)
        {
            _context = context;
            _logger = logger;
        }



        public async Task<ResponseDto<bool>> AddFoodToRestaurant(int restaurantId, AddFoodToRestaurantDto addFood)
        {
            var food = await _context.Foods
                .FirstOrDefaultAsync(f => f.Name.ToLower() == addFood.Name.ToLower() && f.RestaurantId == restaurantId);
            if (food != null)
            {
                return ResponseDto<bool>.Failure("Food with the same name already exists in this restaurant.");
            }

            var newFood = new Food
            {
                Name = addFood.Name,
                Description = addFood.Description,
                Price = addFood.Price,
                RestaurantId = restaurantId,
                CreatedAt = addFood.CreatedAt,
                CreatedBy = addFood.CreatedBy
            };

            _context.Foods.Add(newFood);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Food {newFood.Name} added to restaurant {restaurantId} successfully.");
            return ResponseDto<bool>.SuccessResponse(true, "Food added to restaurant successfully.");
        }

        public async Task<ResponseDto<bool>> CreateRestaurantAsync(CreateRestaurantDto createRestaurantDto)
        {
            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.Name.ToLower() == createRestaurantDto.Name.ToLower() && r.OwnerId == createRestaurantDto.OwnerId);
            if (restaurant != null)
            {
                return ResponseDto<bool>.Failure("Restaurant with the same name already exists for this owner.");
            }

            var newRestaurant = new Restaurant
            {
                Name = createRestaurantDto.Name,
                Address = createRestaurantDto.Address,
                OwnerId = createRestaurantDto.OwnerId,
                CreatedAt = createRestaurantDto.CreatedAt,
                CreatedBy = createRestaurantDto.CreatedBy
            };
            _context.Restaurants.Add(newRestaurant);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Restaurant {newRestaurant.Name} created successfully.");
            return ResponseDto<bool>.SuccessResponse(true, "Restaurant created successfully.");
        }

        public async Task<ResponseDto<bool>> DeleteRestaurantAsync(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return ResponseDto<bool>.Failure("Restaurant not found.");
            }
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Restaurant {restaurant.Name} deleted successfully.");
            return ResponseDto<bool>.SuccessResponse(true, "Restaurant deleted successfully.");
        }

        public async Task<ResponseDto<List<RestaurantDto>>> GetAllRestaurantsAsync()
        {
            var restaurants = await _context.Restaurants
                .Select(r => new RestaurantDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Address = r.Address,
                    OwnerId = r.OwnerId,
                    Foods = r.Foods,
                    Orders = r.Orders,
                    Reviews = r.Reviews,
                    CreatedAt = r.CreatedAt,
                    CreatedBy = r.CreatedBy
                })
                .ToListAsync();
            return ResponseDto<List<RestaurantDto>>.SuccessResponse(restaurants, "Restaurants retrieved successfully.");
        }

        public async Task<ResponseDto<RestaurantDto>> GetRestaurantByIdAsync(int id)
        {
            var restaurant = await _context.Restaurants
                .Where(r => r.Id == id)
                .Select(r => new RestaurantDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Address = r.Address,
                    OwnerId = r.OwnerId,
                    Foods = r.Foods,
                    Orders = r.Orders,
                    Reviews = r.Reviews,
                    CreatedAt = r.CreatedAt,
                    CreatedBy = r.CreatedBy
                })
                .FirstOrDefaultAsync();
            if (restaurant == null)
            {
                return ResponseDto<RestaurantDto>.Failure("Restaurant not found.");
            }
            else
            {
                return ResponseDto<RestaurantDto>.SuccessResponse(restaurant, "Restaurant retrieved successfully.");
            }
        }

        public async Task<ResponseDto<List<RestaurantDto>>> GetRestaurantsByOwnerIdAsync(string ownerId)
        {
            var restaurants = await _context.Restaurants
                .Where(r => r.OwnerId == ownerId)
                .Select(r => new RestaurantDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Address = r.Address,
                    OwnerId = r.OwnerId,
                    Foods = r.Foods,
                    Orders = r.Orders,
                    Reviews = r.Reviews,
                    CreatedAt = r.CreatedAt,
                    CreatedBy = r.CreatedBy
                })
                .ToListAsync();
            return ResponseDto<List<RestaurantDto>>.SuccessResponse(restaurants, "Restaurants retrieved successfully.");
        }

        public async Task<ResponseDto<bool>> UpdateRestaurantAsync(int id, CreateRestaurantDto updateRestaurantDto)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return ResponseDto<bool>.Failure("Restaurant not found.");
            }
            restaurant.Name = updateRestaurantDto.Name;
            restaurant.Address = updateRestaurantDto.Address;
            restaurant.UpdatedAt = updateRestaurantDto.UpdatedAt ?? DateTime.Now;
            restaurant.UpdatedBy = updateRestaurantDto.UpdatedBy;

            _context.Restaurants.Update(restaurant);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Restaurant {restaurant.Name} updated successfully.");
            return ResponseDto<bool>.SuccessResponse(true, "Restaurant updated successfully.");
        }
    }
}
