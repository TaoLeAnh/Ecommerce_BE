using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}