using System.ComponentModel.DataAnnotations;

namespace OnlineFoodDelivery.Dtos.Requests
{
    public class UpdateUserDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public int DepartamentId { get; set; }
    }
}
