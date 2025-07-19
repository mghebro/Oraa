using Microsoft.AspNetCore.Identity;
using ORAA.Enums;

namespace ORAA.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public ACCOUNT_STATUS Status { get; set; } = ACCOUNT_STATUS.CODE_SENT;
        public ROLES Role { get; set; } = ROLES.USER;
        public bool IsVerified { get; set; }
        public string? VerificationCode { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public bool IsActive { get; set; }

        // Authentication Provider Logic
        public string Provider => GetAuthProvider();

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiresAtUtc { get; set; }
        public string? PasswordResetCode { get; set; }
        public string? GoogleId { get; set; }
        public string? AppleId { get; set; }

        // Created and Updated timestamps
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties for related entities
        public UserDetails? UserDetails { get; set; }
        public int? UserDetailsId { get; set; }
        public List<Chat>? Chats { get; set; }
        public List<DiscountCode>? DiscountCodes { get; set; }
        public List<GIftCard>? GIftCards { get; set; }
        public List<Gift>? Gifts { get; set; }
        public Cart? Cart { get; set; }
        public List<Favorite>? Favorites { get; set; }
        public List<Review>? Reviews { get; set; }
        public List<Notification>? Inbox { get; set; }

        // Method to determine authentication provider
        private string GetAuthProvider()
        {
            if (!string.IsNullOrEmpty(AppleId))
                return "Apple";
            if (!string.IsNullOrEmpty(GoogleId))
                return "Google";
            return "Email";
        }

        // Method to update timestamps
        public void UpdateTimestamp()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}