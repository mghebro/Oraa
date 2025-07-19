using System.ComponentModel.DataAnnotations.Schema;

namespace ORAA.Models
{
    public class Admin : User
    {
        public Admin() : base()
        {
        }
        public string Role { get; set; }
        public string Permissions { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public int LoginAttempts { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }

        public List<Blog> Blogs { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<Gift> Gifts { get; set; }

    }
}
