namespace OnlineFoodDelivery.Dtos.Requests
{
    public class AddOrderItemDto
    {
        public int OrderId { get; set; }
        public int FoodId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; } // Price = Food.Price * Quantity
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
