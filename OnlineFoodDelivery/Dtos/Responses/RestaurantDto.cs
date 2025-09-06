using OnlineFoodDelivery.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFoodDelivery.Dtos.Responses
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string OwnerId { get; set; }
        public User Owner { get; set; }
        public ICollection<Food> Foods { get; set; } = new List<Food>();
        public ICollection<Orders> Orders { get; set; } = new List<Orders>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
