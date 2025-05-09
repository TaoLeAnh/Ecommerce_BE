using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Variants")]
    public class Variant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [ForeignKey("Image")]
        public int? ImageId { get; set; }
        public Image? Image { get; set; }

        [Column(TypeName = "int")]
        public int? Quantity { get; set; }

        [Column(TypeName = "float")]
        public double? Price { get; set; }

        [Column(TypeName = "float")]
        public double? SalePrice { get; set; }

        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? VariantCode { get; set; }

        [Column(TypeName = "int")]
        public int? Stock { get; set; }

        public ICollection<OptionProduct> Options { get; set; } = new List<OptionProduct>();
    }
}