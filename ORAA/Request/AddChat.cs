namespace ORAA.Request
{
    public class AddChat
    {
        public string Participants { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public int? LastMessageId { get; set; }
        public bool IsActive { get; set; }
        public int? ConsultantId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
