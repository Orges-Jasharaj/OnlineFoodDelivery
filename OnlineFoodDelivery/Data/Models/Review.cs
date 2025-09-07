using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFoodDelivery.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Client")]
        public string ClientId { get; set; }
        public User Client { get; set; }

        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public int Rating { get; set; } // 1-5
        public string Comment { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
