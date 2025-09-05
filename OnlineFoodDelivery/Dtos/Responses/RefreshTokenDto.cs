namespace OnlineFoodDelivery.Dtos.Responses
{
    public class RefreshTokenDto
    {
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExipirityDate { get; set; }
    }
}
