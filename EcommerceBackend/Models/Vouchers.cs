using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Vouchers")]
    public class Voucher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Code { get; set; }

        [Required]
        public VoucherType VoucherType { get; set; }

        [StringLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        public string? Description { get; set; }

        [Required]
        [ForeignKey("VoucherCondition")]
        public int VoucherConditionId { get; set; }
        public VoucherCondition? VoucherCondition { get; set; }

        [Column(TypeName = "float")]
        public double? ValueCondition { get; set; }

        [Column(TypeName = "float")]
        public double? Discount { get; set; }

        [Column(TypeName = "bit")]
        public bool? Active { get; set; } = true;

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "bit")]
        public bool? Status { get; set; }

        [Required]
        [ForeignKey("Merchant")]
        public int MerchantId { get; set; }
        public Merchant? Merchant { get; set; }

        [Column(TypeName = "int")]
        public int? Quantity { get; set; }
    }
}