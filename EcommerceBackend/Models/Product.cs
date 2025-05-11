using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public virtual Category Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}