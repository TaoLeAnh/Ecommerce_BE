using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Messages")]
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Content { get; set; }

        [Required]
        [ForeignKey("From")]
        public int FromId { get; set; }
        public User? From { get; set; }

        [Required]
        [ForeignKey("To")]
        public int ToId { get; set; }
        public User? To { get; set; }

        [Required]
        public MessageType Type { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
    }
}