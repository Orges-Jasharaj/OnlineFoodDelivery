using OnlineFoodDelivery.Data.Models;

namespace OnlineFoodDelivery.Dtos.Requests
{
    public class AddFoodToRestaurantDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
