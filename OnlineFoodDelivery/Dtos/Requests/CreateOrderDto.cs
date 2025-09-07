namespace OnlineFoodDelivery.Dtos.Requests
{
    public class CreateOrderDto
    {
        public string ClientId { get; set; }
        public string DriverId { get; set; }    
        public string Status { get; set; }
        public double TotalPrice { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
