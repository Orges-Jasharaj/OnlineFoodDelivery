namespace OnlineFoodDelivery.Dtos.Requests
{
    public class CreateReviewDto
    {
        public string ClientId { get; set; }
        public int RestaurantId { get; set; }
        public int Rating { get; set; } // 1-5
        public string Comment { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
