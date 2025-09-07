namespace OnlineFoodDelivery.Dtos.Requests
{
    public class UpdatePriceOfFoodDto
    {
        public string OwnerId { get; set; }
        public int FoodId { get; set; }
        public int RestaurantId { get; set; }
        public double NewPrice { get; set; }
        public string UpdatedBy { get; set; }   
        public DateTime UpdatedAt { get; set; }
    }
}
