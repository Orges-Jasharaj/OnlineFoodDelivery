namespace OnlineFoodDelivery.Dtos.Requests
{
    public class CreateRestaurantDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string OwnerId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
