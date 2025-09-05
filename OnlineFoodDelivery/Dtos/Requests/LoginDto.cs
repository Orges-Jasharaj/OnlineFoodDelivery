using System.ComponentModel.DataAnnotations;

namespace OnlineFoodDelivery.Dtos.Requests
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
