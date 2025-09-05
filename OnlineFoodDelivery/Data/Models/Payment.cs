using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFoodDelivery.Data.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Orders Order { get; set; }

        public double Amount { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
