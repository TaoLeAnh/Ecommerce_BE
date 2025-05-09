using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Merchants")]
    public class Merchant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        [Required]
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address? Address { get; set; }

        [StringLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        public string? Description { get; set; }

        [ForeignKey("Logo")]
        public int? LogoId { get; set; }
        public Image? Logo { get; set; }

        [ForeignKey("Background")]
        public int? BackgroundId { get; set; }
        public Image? Background { get; set; }

        [StringLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string? Email { get; set; }

        [StringLength(20)]
        [Column(TypeName = "nvarchar(20)")]
        public string? PhoneNumber { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();

        [Column(TypeName = "bit")]
        public bool? IsRoyal { get; set; }

        [Column(TypeName = "float")]
        public double? Rating { get; set; }

        [Column(TypeName = "float")]
        public double? TotalSold { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? LastAccess { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Column(TypeName = "bit")]
        public bool? Status { get; set; }
    }
}