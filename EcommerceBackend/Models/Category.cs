using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public virtual ICollection<Product> Products { get; set; }
    }
}