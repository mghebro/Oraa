namespace ORAA.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string MessageType { get; set; }
        public string Attachments { get; set; }
        public bool IsRead { get; set; }
        public string ReadBy { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int ChatId { get; set; }
        public Chat Chat { get; set; }

        public int SenderUserId { get; set; }
        public User Sender { get; set; }
    }
}
