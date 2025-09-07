namespace OnlineFoodDelivery.Dtos.Requests
{
    public class MakePaymentDto
    {
        public int OrderId { get; set; }
        public double Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
