using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFoodDelivery.Dtos.Requests
{
    public class CreateFoodDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int RestaurantId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
