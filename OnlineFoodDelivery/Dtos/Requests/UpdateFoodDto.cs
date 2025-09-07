namespace OnlineFoodDelivery.Dtos.Requests
{
    public class UpdateFoodDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
