using System.ComponetModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string Address { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public System.DateTime Date { get; set; } = System.DateTime.Now;

        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public System.DateTime LastTimeUpdated { get; set; } = System.DateTime.Now;

        [Required]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string OrderCode { get; set; }

        [Required]
        [Column(TypeName = "tinyint")]
        public byte PaymentType { get; set; }

        [Required]
        [StringLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string Phone { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? Status { get; set; } = "Pending";

        [Required]
        [Column(TypeName = "float")]
        public double Total { get; set; }

        public ICollection<OrderProduct> Products { get; set; } = new List<OrderProduct>();

        [Required]
        [ForeignKey("Merchant")]
        public int MerchantId { get; set; }

        public Merchant? Merchant { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required]
        [ForeignKey("Voucher")]
        public int VoucherId { get; set; } = 0;
        public Voucher? Voucher { get; set; } = null;
    }
}