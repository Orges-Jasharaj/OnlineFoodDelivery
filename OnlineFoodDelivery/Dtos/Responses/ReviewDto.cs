using OnlineFoodDelivery.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFoodDelivery.Dtos.Responses
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public User Client { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public int Rating { get; set; } 
        public string Comment { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
