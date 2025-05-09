using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        [StringLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        public string? Description { get; set; }

        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? ProductCode { get; set; }

        [Column(TypeName = "bigint")]
        public long? Sold { get; set; }

        [Column(TypeName = "bit")]
        public bool? IsDiscount { get; set; }

        [Column(TypeName = "float")]
        public double? Rating { get; set; }

        [Column(TypeName = "bit")]
        public bool? Status { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column(TypeName = "datetime")]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [Required]
        [ForeignKey("Merchant")]
        public int MerchantId { get; set; }
        public Merchant? Merchant { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public ICollection<Image> Images { get; set; } = new List<Image>();
        public ICollection<GroupOption> GroupOptions { get; set; } = new List<GroupOption>();
    }
}
