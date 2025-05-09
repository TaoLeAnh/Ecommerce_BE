using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string Username { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string Password { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool Enabled { get; set; } = true;

        [StringLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string? Email { get; set; }

        [StringLength(20)]
        [Column(TypeName = "nvarchar(20)")]
        public string? PhoneNumber { get; set; }

        [ForeignKey("Avatar")]
        public int? AvatarId { get; set; }
        public Image? Avatar { get; set; }

        public ICollection<Role> Roles { get; set; } = new List<Role>();
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
