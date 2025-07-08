using ORAA.Enums;

namespace ORAA.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ACCOUNT_STATUS Status { get; set; }
        public ROLES Role { get; set; }
        public string Email { get; set; }
    }
}
