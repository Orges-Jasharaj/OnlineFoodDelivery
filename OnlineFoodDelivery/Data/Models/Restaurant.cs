using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFoodDelivery.Data.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }

        [ForeignKey("Owner")]
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
