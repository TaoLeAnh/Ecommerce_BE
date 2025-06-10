namespace EcommerceBackend.Models
{
    public class Review
    {
        public int? ReviewId { get; set; } 
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; } // Add this property
        
        // Navigation properties
        public User? User { get; set; }
        public Product? Product { get; set; }
    }
}