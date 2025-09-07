using OnlineFoodDelivery.Data.Models;

namespace OnlineFoodDelivery.Dtos.Responses
{
    public class RestaurantOrdersDto
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public int TotalOrders { get; set; }
        public ICollection<Orders> Orders { get; set; } = new List<Orders>();
    }
}
