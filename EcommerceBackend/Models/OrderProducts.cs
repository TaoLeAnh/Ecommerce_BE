using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("OrderProducts")]
    public class OrderProduct
    {
        [Key]
        public ProductOrderPK ProductOrderPK { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "float")]
        public double Price { get; set; }

        public double GetPrices()
        {
            return ProductOrderPK.Variants.Price * Quantity;
        }

        public double GetPrice()
        {
            return Price;
        }

        public Product GetProduct()
        {
            return ProductOrderPK.Variant.Product;
        }
    }
}