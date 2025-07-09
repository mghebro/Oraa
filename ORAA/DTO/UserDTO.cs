using ORAA.Enums;

namespace ORAA.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public bool IsVerified { get; set; }

        public bool EmailNotifications { get; set; }

        public bool SmsNotifications { get; set; }

        public bool PushNotifications { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastLoginAt { get; set; }

        public string Role { get; set; } = ROLES.USER.ToString();
    }
}
