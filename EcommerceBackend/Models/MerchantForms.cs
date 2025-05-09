using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("MerchantForms")]
    public class MerchantForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [ForeignKey("Merchant")]
        public int MerchantId { get; set; }
        public Merchant? Merchant { get; set; }

        [Required]
        public FormType FormType { get; set; }

        [Required]
        public FormStatus Status { get; set; }

        [Required]
        public RoyalUser CurrentStatus { get; set; }

        [Required]
        public RoyalUser RequestedStatus { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime SubmissionDate { get; set; }

        [Column(TypeName = "varbinary(max)")]
        public byte[]? Documents { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
    }
}