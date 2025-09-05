using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFoodDelivery.Data.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Orders Order { get; set; }

        [ForeignKey("Food")]
        public int FoodId { get; set; }
        public Food Food { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; } // Price = Food.Price * Quantity

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
