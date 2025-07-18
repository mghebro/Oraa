
using System.ComponentModel.DataAnnotations;

namespace ORAA.Models.Apple
{
    public class AppleUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string AppleId { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Email { get; set; }

        [StringLength(255)]
        public string? Name { get; set; }

        public bool IsPrivateEmail { get; set; } = false;

        [StringLength(1000)]
        public string? RefreshToken { get; set; }

        [StringLength(1000)]
        public string? AccessToken { get; set; }

        public DateTime? CreatedAt { get; set; }

        public bool EmailVerified { get; set; } = false;

        public DateTime? AuthTime { get; set; }

        public int? ExpiresIn { get; set; }

        [StringLength(50)]
        public string? TokenType { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;
    }
}
