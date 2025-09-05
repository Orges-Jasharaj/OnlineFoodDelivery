using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFoodDelivery.Data.Models
{
    public class Food
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }

        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
