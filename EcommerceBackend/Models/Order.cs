using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Address { get; set;}
        public string Note{ get; set;}
        public decimal ShipCost {get;set;}
        public decimal GrandTotal{get;set;}

        // Navigation properties
        public virtual User? User { get; set; }
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
        public virtual ICollection<Payment>? Payments { get; set; }
    }
}