using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string TransactionId { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime PaidAt { get; set; }
        // Navigation properties
        public virtual Order? Order { get; set; }
    }
}