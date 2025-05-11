using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}