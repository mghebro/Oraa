namespace ORAA.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string Permissions { get; set; }
        public int LoginAttempts { get; set; }
        public int? CreatedBy { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}