using OnlineFoodDelivery.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFoodDelivery.Dtos.Responses
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Client")]
        public string ClientId { get; set; }
        public User Client { get; set; }

        [ForeignKey("Driver")]
        public string? DriverId { get; set; }
        public User Driver { get; set; }
        public string Status { get; set; }
        public double TotalPrice { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
